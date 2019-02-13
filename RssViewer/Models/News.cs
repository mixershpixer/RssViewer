using System;
using System.Collections.Generic;

namespace RssViewer.Models
{
    public partial class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Discription { get; set; }
        public string NewsUrl { get; set; }
        public int SourceId { get; set; }
        public Source Source { get; set; }
    }
}
