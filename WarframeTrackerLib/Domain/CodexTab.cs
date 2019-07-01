using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TowerSoft.Repository.Attributes;

namespace WarframeTrackerLib.Domain {
    public class CodexTab {
        [Autonumber]
        public int ID { get; set; }

        [Display(Name = "Codex Section")]
        public CodexSection CodexSectionID { get; set; }

        [Required, MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(45), Display(Name = "Image Name")]
        public string ImageName { get; set; }

        [Display(Name = "Sorting Priority")]
        public int SortingPriority { get; set; }

        [Display(Name = "Group Items by Item Category")]
        public bool GroupItemsByItemCategory { get; set; }


        // Navigation Properties
        //public virtual List<CodexTabItemCategory> CodexTabItemCategory_Objects { get; set; }
        public virtual List<ItemCategory> ItemCategory_Objects { get; set; }
    }
}
