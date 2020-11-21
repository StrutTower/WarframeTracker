using System;
using System.Collections.Generic;
using System.Text;

namespace WarframeTrackerLib.WarframeApi.WorldState {
    public class CetusCycle {
        public DateTime Activation { get; set; }

        public DateTime Expiry { get; set; }

        public bool IsDay { get; set; }
    }
}
