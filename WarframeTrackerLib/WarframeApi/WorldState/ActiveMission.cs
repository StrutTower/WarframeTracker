using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;
using WarframeTrackerLib.Utilities;

namespace WarframeTrackerLib.WarframeApi.WorldState {
   public class ActiveMission {
        public string ID { get; set; }

        public int Region { get; set; }

        public string Node { get; set; }

        public SolNode SolNode { get; set; }

        public string MissionType { get; set; }

        public string Modifier { get; set; }

        public DateTime StartDateUtc { get; set; }

        public DateTime EndDateUtc { get; set; }


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
