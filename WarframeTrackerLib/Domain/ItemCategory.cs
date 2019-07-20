using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TowerSoft.Repository.Attributes;

namespace WarframeTrackerLib.Domain {
    public class ItemCategory {
        [Autonumber]
        public int ID { get; set; }

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


        [Display(Name = "Manually Excluded Unique Item Names", Description = "Comma separated list of unique item names")]
        public string ManuallyExcludedUniqueNames { get; set; }

        public virtual CodexTab CodexTab_Object { get; set; }


        public List<string> ManuallyExcludedUniqueNamesList {
            get {
                List<string> list = new List<string>();
                if (!string.IsNullOrWhiteSpace(ManuallyExcludedUniqueNames)) {
                    string[] array = ManuallyExcludedUniqueNames.Split(new[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                    foreach (string str in array) {
                        list.Add(str.Trim());
                    }
                }
                return list;
            }
        }
    }
}
