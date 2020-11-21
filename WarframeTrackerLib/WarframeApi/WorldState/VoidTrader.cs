using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using WarframeTrackerLib.Utilities;

namespace WarframeTrackerLib.WarframeApi.WorldState {
    public class VoidTrader {
        [JsonProperty("Character")]
        public string Name { get; set; }

        public string Node { get; set; }

        public SolNode SolNode { get; set; }

        public List<VoidTraderManifestItem> Manifest { get; set; }

        public DateTime StartDateUtc { get; set; }

        public DateTime EndDateUtc { get; set; }

        public int ItemCount {
            get {
                if (Manifest != null) return Manifest.Count;
                return 0;
            }
        }

        public string TimeTillDisplay {
            get {
                if (IsHere) return "Currently Here.";
                string output = string.Empty;
                TimeSpan time = StartDateUtc.Subtract(DateTime.UtcNow);
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
                if (DateTime.UtcNow.IsBetween(StartDateUtc, EndDateUtc)) {
                    return true;
                }
                return false;
            }
        }

        #region Manual Deserialization
        [JsonExtensionData]
        [SuppressMessage("Style", "IDE0044", Justification = "Readonly prevents Newtonsoft.Json from setting the data.")]
#pragma warning disable 0649
        private IDictionary<string, JToken> _additionalData;
#pragma warning restore 0649

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            long startMilliseconds = JsonHelper.ConvertPath<long>(_additionalData, "Activation.$date.$numberLong");
            StartDateUtc = JsonHelper.MillisecondsToDateTimeUtc(startMilliseconds);

            long endMilliseconds = JsonHelper.ConvertPath<long>(_additionalData, "Expiry.$date.$numberLong");
            EndDateUtc = JsonHelper.MillisecondsToDateTimeUtc(endMilliseconds);
        }
        #endregion
    }
}
