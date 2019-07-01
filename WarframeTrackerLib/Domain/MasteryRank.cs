using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TowerSoft.Repository.Attributes;

namespace WarframeTrackerLib.Domain {
    public class MasteryRank {
        [Autonumber]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public int Number { get; set; }

        [Display(Name = "Experience Required")]
        public int ExperienceRequired { get; set; }


        [NotMapped]
        public int? NextRankExperience_ { get; set; }
    }
}
