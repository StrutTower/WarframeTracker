using System.ComponentModel.DataAnnotations;
using TowerSoft.Repository.Attributes;

namespace WarframeTrackerLib.Domain {
    public class ManualItemData {
        [Autonumber]
        public int ID { get; set; }
        
        [Required, Display(Name = "Item Name")]
        public string ItemUniqueName { get; set; }

        [Required, Display(Name = "Property Name")]
        public string PropertyName { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
