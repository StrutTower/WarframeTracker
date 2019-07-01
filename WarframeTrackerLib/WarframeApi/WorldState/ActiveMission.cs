using System;
using System.Collections.Generic;
using System.Text;
using WarframeTrackerLib.Utilities;

namespace WarframeTrackerLib.WarframeApi.WorldState {
   public class ActiveMission {
        [JsonDeserializeProperty("_id.$oid")]
        public string ID { get; set; }

        public int Region { get; set; }

        public string Node { get; set; }

        public SolNode SolNode { get; set; }

        public string MissionType { get; set; }

        public string Modifier { get; set; }

        public DateTime StartedOnUtc { get; set; }

        [JsonDeserializeProperty("Activation.$date.$numberLong")]
        public long StartedOnUtcEpoch {
            get {
                return (long)StartedOnUtc.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            }
            set {
                StartedOnUtc = new DateTime(1970, 1, 1).AddMilliseconds(value);
            }
        }
        public DateTime StartedOnLocalTime {
            get { return StartedOnUtc.ToLocalTime(); }
        }

        public DateTime EndsOnUtc { get; set; }

        [JsonDeserializeProperty("Expiry.$date.$numberLong")]
        public long EndsOnUtcEpoch {
            get {
                return (long)StartedOnUtc.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            }
            set {
                StartedOnUtc = new DateTime(1970, 1, 1).AddMilliseconds(value);
            }
        }

        public DateTime EndsOnLocalTime {
            get { return EndsOnUtc.ToLocalTime(); }
        }
    }
}
