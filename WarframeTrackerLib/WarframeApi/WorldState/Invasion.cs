using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using WarframeTrackerLib.Utilities;

namespace WarframeTrackerLib.WarframeApi.WorldState {
    public class Invasion {
        public string ID { get; set; }

        public string Faction { get; set; }

        public string Node { get; set; }

        public SolNode SolNode { get; set; }

        public int Count { get; set; }

        public int Goal { get; set; }

        public string LocTag { get; set; }

        public bool Completed { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsImportant { get; set; }

        public DateTime StartDateUtc { get; set; }

        public string AttackerFaction { get; set; }

        public List<InvasionRewardItem> AttackerRewardItems { get; set; }

        public string DefenderFaction { get; set; }

        public List<InvasionRewardItem> DefenderRewardItems { get; set; }


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

            AttackerRewardItems = JsonHelper.ConvertPath<List<InvasionRewardItem>>(_additionalData, "AttackerReward.countedItems");
            AttackerFaction = JsonHelper.ConvertPath<string>(_additionalData, "AttackerMissionInfo.faction");

            DefenderRewardItems = JsonHelper.ConvertPath<List<InvasionRewardItem>>(_additionalData, "DefenderReward.countedItems");
            DefenderFaction = JsonHelper.ConvertPath<string>(_additionalData, "DefenderMissionInfo.faction");
        }
        #endregion
    }
}
