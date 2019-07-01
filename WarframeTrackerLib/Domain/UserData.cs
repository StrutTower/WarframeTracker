using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WarframeTrackerLib.Domain {
    public class UserData {
        [Key]
        public int UserID { get; set; }

        [Range(0, 13), Display(Name = "Junctions Completed", Description = "Each completed junction gives 1,000 mastery experience.")]
        public int JunctionsCompleted { get; set; }

        [Range(0, 227), Display(Name = "Mission Nodes Completed", Description = "Number of mission nodes completed on the starchart. This number is listed in the in-game profile. Each node gives about 64 experience.")]
        public int MissionNodesCompleted { get; set; }
    }
}
