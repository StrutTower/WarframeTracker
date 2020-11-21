using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TowerSoft.Repository.Attributes;

namespace WarframeTrackerLib.Domain {
    public class AmpComponent {
        [Autonumber]
        public int ID { get; set; }

        [Required]
        public AmpComponentType AmpComponentType { get; set; }

        public int Tier { get; set; }

        [Required, MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required, MaxLength(255)]
        public string ImageUrl { get; set; }

        [MaxLength(45)]
        public string TriggerType { get; set; }

        public int? MaximumRange { get; set; }

        public double? RoundsPerSecond { get; set; }

        public double? Accuracy { get; set; }

        public int? DirectDamage { get; set; }

        public int? AreaDamage { get; set; }

        public double? CritChance { get; set; }

        public double? CritMultiplier { get; set; }

        public double? StatusChance { get; set; }

        public int? AmmoPerShot { get; set; }

        public double? EnergyConsumptionPerSecond { get; set; }

        public double? RechargeDelay { get; set; }

        public double? RechargePerSecond { get; set; }
    }
}
