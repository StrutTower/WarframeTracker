using System.ComponentModel.DataAnnotations;
using TowerSoft.Repository.Attributes;

namespace WarframeTrackerLib.Domain {
    public class ItemCategory {
        [Autonumber]
        public int ID { get; set; }

        //[Display(Name = "Category Type")]
        //public CategoryType? CategoryTypeID { get; set; }
        [Display(Name = "Codex Tab")]
        public int CodexTabID { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Warframe API Category Type")]
        public string WarframeApiCategoryName { get; set; }

        [Display(Name = "Warframe API Unique Name Regex Filter")]
        public string WarframeApiUniqueNameRegexFilter { get; set; }

        [Display(Name = "Allow Acquisition")]
        public bool AllowAcquisition { get; set; }

        [Display(Name = "Can Be Mastered")]
        public bool CanBeMastered { get; set; }

        [Display(Name = "Max Rank Experience")]
        public int? MaxRankExperience { get; set; }

        [Display(Name = "Sorting")]
        public int SortingPriority { get; set; }


        public virtual CodexTab CodexTab_Object { get; set; }
    }
}
