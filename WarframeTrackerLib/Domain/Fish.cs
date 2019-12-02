using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TowerSoft.Repository.Attributes;
using WarframeTrackerLib.WarframeApi;

namespace WarframeTrackerLib.Domain {
    public class Fish {
        [Autonumber]
        public int ID { get; set; }

        [Required, MaxLength(255)]
        public string UniqueName { get; set; }

        [Required, MaxLength(75)]
        public string MapName { get; set; }

        [Required, MaxLength(45)]
        public string Biome { get; set; }

        [Required, MaxLength(45)]
        public string ActiveTime { get; set; }

        [Required, MaxLength(45)]
        public string SpearType { get; set; }

        [Required, MaxLength(45)]
        public string Rarity { get; set; }

        public int MaxWeight { get; set; }

        [MaxLength(45)]
        public string BaitName { get; set; }

        public int SmallStanding { get; set; }

        public int MediumStanding { get; set; }

        public int LargeStanding { get; set; }

        
        public virtual WarframeItem WarframeItem_Object { get; set; }
    }
}
