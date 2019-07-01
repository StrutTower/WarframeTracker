using System;
using System.ComponentModel.DataAnnotations;

namespace WarframeTrackerLib.Domain {
    public class ItemCache {
        [Key, Required]
        public string UniqueName { get; set; }

        public int ItemCategoryID { get; set; }

        public string Name { get; set; }

        public string Data { get; set; }

        public DateTime? UpdatedTimestamp { get; set; }
    }
}
