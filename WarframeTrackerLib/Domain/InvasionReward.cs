using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TowerSoft.Repository.Attributes;

namespace WarframeTrackerLib.Domain {
    public class InvasionReward {
        [Autonumber]
        public int ID { get; set; }

        [Required, MaxLength(150)]
        public string UniqueName { get; set; }

        [Required, MaxLength(45)]
        public string Name { get; set; }

        [Display(Name = "Show on Home View")]
        public bool ShowOnHomeView { get; set; }
    }
}
