using System.ComponentModel.DataAnnotations;

namespace WarframeTrackerLib.Domain {
    public class ItemAcquisition {
        [Key]
        public int UserID { get; set; }

        [Key, Required]
        public string ItemUniqueName { get; set; }

        public bool IsAcquired { get; set; }

        public bool IsMastered { get; set; }
    }
}
