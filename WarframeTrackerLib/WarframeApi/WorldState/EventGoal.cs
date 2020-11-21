using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;
using WarframeTrackerLib.Utilities;

namespace WarframeTrackerLib.WarframeApi.WorldState {
    public class EventGoal {
        public string ID { get; set; }

        public DateTime StartDateUtc { get; set; }

        public DateTime EndDateUtc { get; set; }

        public int Count { get; set; }

        public int Goal { get; set; }

        public List<int> InterimGoals { get; set; }

        public bool Personal { get; set; }

        public bool ClampNodeScores { get; set; }

        public string Node { get; set; }

        public string MissionKeyName { get; set; }

        public string Faction { get; set; }

        [JsonProperty("Desc")]
        public string Description { get; set; }

        public string Tooltip { get; set; }

        public string Icon { get; set; }

        public string Tag { get; set; }

        public List<EventReward> InterimRewards { get; set; }

        public EventReward Reward { get; set; }


        #region Manual Deserialization
        [JsonExtensionData]
        [SuppressMessage("Style", "IDE0044", Justification = "Readonly prevents Newtonsoft.Json from setting the data.")]
#pragma warning disable 0649
        private IDictionary<string, JToken> _additionalData;
#pragma warning restore 0649

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            ID = JsonHelper.ConvertPath<string>(_additionalData, "_id.$oid");

            long startMilliseconds = JsonHelper.ConvertPath<long>(_additionalData, "Activation.$date.$numberLong");
            StartDateUtc = JsonHelper.MillisecondsToDateTimeUtc(startMilliseconds);


            long endMilliseconds = JsonHelper.ConvertPath<long>(_additionalData, "Expiry.$date.$numberLong");
            EndDateUtc = JsonHelper.MillisecondsToDateTimeUtc(endMilliseconds);
        }
        #endregion
    }
}
