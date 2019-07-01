using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using WarframeTrackerLib.Utilities;

namespace WarframeTrackerLib.WarframeApi.WorldState {
    //[JsonConverter(typeof(JsonPathConverter<EventGoal>))]
    public class EventGoal {
        [JsonDeserializeProperty("_id.$oid")]
        public string ID { get; set; }

        [JsonDeserializeProperty("Activation.$date.$numberLong")]
        public long StartEpoch { get; set; }

        [JsonDeserializeProperty("Expiry.$date.$numberLong")]
        public long EndEpoch { get; set; }

        public int Count { get; set; }

        public int Goal { get; set; }

        public List<int> InterimGoals { get; set; }

        public bool Personal { get; set; }

        public bool ClampNodeScores { get; set; }

        public string Node { get; set; }

        public string MissionKeyName { get; set; }

        public string Faction { get; set; }

        [JsonDeserializeProperty("Desc")]
        public string Description { get; set; }

        public string Tooltip { get; set; }

        public string Icon { get; set; }

        public string Tag { get; set; }

        public List<EventReward> InterimRewards { get; set; }

        public EventReward Reward { get; set; }
    }
}
