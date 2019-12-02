using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeTrackerLib.Utilities;

namespace WarframeTrackerLib.WarframeApi.WorldState {
    [JsonConverter(typeof(JsonPathConverter<VoidTrader>))]
    public class VoidTrader {
        [JsonDeserializeProperty("Character")]
        public string Name { get; set; }

        public string Node { get; set; }

        public SolNode SolNode { get; set; }

        [JsonDeserializeProperty("Activation.$date.$numberLong")]
        public long StartEpoch { get; set; }

        [JsonDeserializeProperty("Expiry.$date.$numberLong")]
        public long EndEpoch { get; set; }

        public List<VoidTraderManifestItem> Manifest { get; set; }

        public int ItemCount {
            get {
                if (Manifest != null) return Manifest.Count;
                return 0;
            }
        }

        public DateTime UtcStartDate {
            get {
                DateTime date = new DateTime(1970, 1, 1).AddMilliseconds(StartEpoch);
                return date;
            }
        }

        public DateTime LocalStartDate {
            get {
                return UtcStartDate.ToLocalTime();
            }
        }

        public DateTime UtcEndDate {
            get {
                DateTime date = new DateTime(1970, 1, 1).AddMilliseconds(EndEpoch);
                return date;
            }
        }

        public DateTime LocalEndDate {
            get {
                return UtcEndDate.ToLocalTime();
            }
        }

        public string TimeTillDisplay {
            get {
                if (IsHere) return "Currently Here.";
                string output = string.Empty;
                TimeSpan time = UtcStartDate.Subtract(DateTime.UtcNow);
                if (time.TotalDays >= 1) {
                    int days = (int)Math.Floor(time.TotalDays);
                    output += days + "d";
                }
                if (time.TotalDays >= 1 && time.Hours > 0) {
                    output += " ";
                }
                if (time.Hours > 0) {
                    output += time.Hours + "h";
                }
                if (string.IsNullOrWhiteSpace(output)) return "<1h";
                return output;
            }
        }

        public bool IsHere {
            get {
                if (DateTime.UtcNow.IsBetween(UtcStartDate, UtcEndDate)) {
                    return true;
                }
                return false;
            }
        }
    }

}
