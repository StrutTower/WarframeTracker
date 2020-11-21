using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository.Attributes;

namespace WarframeTrackerLib.Domain {
    public class WikiPage {
        [Autonumber]
        public int ID { get; set; }

        public WikiPageType WikiPageType { get; set; }

        public string Title { get; set; }

        public string MarkdownContent { get; set; }

        public bool IsIndexPage { get; set; }

        public bool IsActive { get; set; }
    }
}
