using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeTrackerLib.Utilities;

namespace WarframeTrackerLib.WarframeApi.WorldState {
    public class Invasion {
        public string OID { get; set; }

        [JsonProperty("_id")]
        public JToken OIDToken {
            set { OID = value["$oid"].ToString(); }
        }

        public string Faction { get; set; }

        public string Node { get; set; }

        public SolNode SolNode { get; set; }

        public int Count { get; set; }

        public int Goal { get; set; }

        public string LocTag { get; set; }

        public bool Completed { get; set; }

        public List<InvasionRewardItem> AttackerRewardItems { get; set; }

        [JsonProperty("AttackerReward")]
        public JToken AttackerRewardJToken {
            set {
                if (value is JArray) {

                } else if (value["countedItems"] != null) {
                    AttackerRewardItems = value["countedItems"].ToObject<List<InvasionRewardItem>>();
                }
            }
        }

        //Attacker Mission Info
        public string AttackerFaction { get; set; }

        public JToken AttackerMissionInfo {
            set { AttackerFaction = value["faction"].ToString(); }
        }

        public List<InvasionRewardItem> DefenderRewardItems { get; set; }

        [JsonProperty("DefenderReward")]
        public JToken DefenderRewardJToken {
            set {
                if (value is JArray) {

                } else if (value["countedItems"] != null) {
                    DefenderRewardItems = value["countedItems"].ToObject<List<InvasionRewardItem>>();
                }
            }
        }

        //Defender Mission Info
        public string DefenderFaction { get; set; }

        public JToken DefenderMissionInfo {
            set { DefenderFaction = value["faction"].ToString(); }
        }

        [JsonProperty("Activation.$date.$numberLong")]
        public long ActivationEpochUtc { get; set; }

        public DateTime ActivateDateUtc {
            get {
                return new DateTime(1970, 1, 1).AddMilliseconds(ActivationEpochUtc);
            }
        }

        public JToken Activation {
            set { ActivationEpochUtc = value["$date"]["$numberLong"].ToObject<long>(); }
        }
    }
}
