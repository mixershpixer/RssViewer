using PagedList;
using RssViewer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RssViewer.Controllers
{
    public class HomeController : Controller
    {
        RssReadContext db = new RssReadContext();
        public static List<News> allNews = new List<News>();

        [HttpGet]
        public ActionResult Index()
        {
            List<Source> sources = new List<Source>();
            Source source = new Source { SourceId = 0, Url = "allnews.by", Name = "Все", News = null };
            sources.Add(source);
            foreach (Source s in db.Sources)
            {
                sources.Add(s);
            }
            SelectList sourcesList = new SelectList(sources, "SourceId", "Name");
            ViewBag.Source = sourcesList;
            IQueryable<News> news = db.News.Include(x => x.Source).OrderByDescending(x => x.PublicationDate);
            allNews.Clear();
            foreach (News n in news)
            {
                allNews.Add(n);
            }
            ViewBag.News = news.Take(10);
            ViewData["Count"] = news.Count() / 10;
            return View();
        }
        [HttpPost]
        public ActionResult Index(int Source, string order)
        {
            IQueryable<News> news;
            List<Source> sources = new List<Source>();
            Source source = new Source { SourceId = 0, Url = "", Name = "Все", News = null};
            sources.Add(source);
            foreach (Source s in db.Sources)
            {
                sources.Add(s);
            }
            SelectList sourcesList = new SelectList(sources, "SourceId", "Name");
            ViewBag.Source = sourcesList;
            if (Source == 0)
            {
                if (order == "date")
                    news = db.News.Include(x => x.Source).OrderByDescending(x => x.PublicationDate);
                else if (order == "source")
                    news = db.News.Include(x => x.Source).OrderBy(x => x.Source.SourceId);
                else news = db.News.Include(x => x.Source);
            }
            else
            {
                if (order == "date")
                    news = db.News.Include(x => x.Source).Where(x => x.SourceId == Source).OrderByDescending(x => x.PublicationDate);
                else if (order == "source")
                    news = db.News.Include(x => x.Source).Where(x => x.SourceId == Source).OrderBy(x => x.Source.SourceId);
                else news = db.News.Include(x => x.Source).Where(x => x.SourceId == Source);
            }
            ViewBag.News = news.Take(10); 
            ViewData["Count"] = news.Count() / 10;
            allNews.Clear();
            foreach (News n in news)
            {
                allNews.Add(n);
            }
            return View();
        }
        [HttpGet]
        public ActionResult NewsView()
        {
            return View();
        }
        [HttpPost]
        public PartialViewResult NewsView(Number model)
        {
            int pageNumber = model.Num;
            ViewData["Count"] = allNews.Count() / 10;
            ViewBag.News = allNews.Skip(pageNumber * 10).Take(10);
            return PartialView("NewsView");
        }
    }
}