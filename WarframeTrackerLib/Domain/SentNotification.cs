using System;
using System.Collections.Generic;
using System.Text;
using TowerSoft.Repository.Attributes;

namespace WarframeTrackerLib.Domain {
    public class SentNotification {
        [Autonumber]
        public int ID { get; set; }
        public string Type { get; set; }
        public string DataID { get; set; }
    }
}
