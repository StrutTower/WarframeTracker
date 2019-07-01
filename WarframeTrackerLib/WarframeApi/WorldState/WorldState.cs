using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeTrackerLib.Utilities;

namespace WarframeTrackerLib.WarframeApi.WorldState {
    public class WorldState {
        public List<VoidTrader> VoidTraders { get; set; }

        public List<ActiveMission> ActiveMissions { get; set; }

        public List<Invasion> Invasions { get; set; }

        public List<EventGoal> Goals { get; set; }
    }
}
