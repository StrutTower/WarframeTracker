using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TowerSoft.Repository.Attributes;
using WarframeTrackerLib.WarframeApi;

namespace WarframeTrackerLib.Domain {
    public class ModSuggestion {
        [Autonumber]
        public int ID { get; set; }

        [Required, Display(Name = "Mod Category")]
        public ModCategory ModCategory { get; set; }

        [Display(Name = "Predecessor Mod Suggestion")]
        [Description("If this mod is an upgraded version, this is the base mod")]
        public int? PredecessorModSuggestionID { get; set; }

        [Required, MaxLength(200), Display(Name = "Mod Name")]
        public string UniqueName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(255), Display(Name = "Drop Location")]
        public string DropLocation { get; set; }

        public string WarframeItemJson { get; set; }

        //***
        private WarframeItem _warframeItem;
        public WarframeItem WarframeItem {
            get {
                if (!string.IsNullOrWhiteSpace(WarframeItemJson)) {
                    if (_warframeItem == null) {
                        _warframeItem = JsonConvert.DeserializeObject<WarframeItem>(WarframeItemJson);
                    }
                    return _warframeItem;
                }
                return null;
            }
        }

        [NotMapped]
        public List<ModSuggestion> UpgradeMod_Objects { get; set; }
    }
}
