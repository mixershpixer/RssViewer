using System;
using System.Collections.Generic;

namespace RssViewer.Models
{
    public partial class Source
    {
        public int SourceId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public ICollection<News> News { get; set; }
    }
}
