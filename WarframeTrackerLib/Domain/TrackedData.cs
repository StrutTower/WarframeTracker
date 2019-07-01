using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository.Attributes;

namespace WarframeTrackerLib.Domain {
    public class TrackedData {
        [Autonumber]
        public int ID { get; set; }
        public string Type { get; set; }
        public string DataID { get; set; }
        public string Data { get; set; }
    }
}
