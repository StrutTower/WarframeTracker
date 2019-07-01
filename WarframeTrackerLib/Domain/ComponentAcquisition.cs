using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarframeTrackerLib.Domain {
    public class ComponentAcquisition {
        [Key]
        public int UserID { get; set; }

        [Key, Required]
        public string ComponentUniqueName { get; set; }

        [Key, Required]
        public string ItemUniqueName { get; set; }

        public bool IsAcquired { get; set; }


        [NotMapped]
        public string ComponentName { get; set; }
    }
}
