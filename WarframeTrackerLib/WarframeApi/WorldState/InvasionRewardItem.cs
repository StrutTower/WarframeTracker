using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarframeTrackerLib.WarframeApi.WorldState {
    public class InvasionRewardItem {
        [JsonProperty("ItemType")]
        public string ItemUniqueName { get; set; }

        public int ItemCount { get; set; }

        [JsonIgnore]
        public string Name { get; set; }


        public override string ToString() {
            if (string.IsNullOrWhiteSpace(Name))
                return (ItemCount > 1 ? ItemCount.ToString() + " " : "") + ItemUniqueName.Split('/').Last();
            else
                return (ItemCount > 1 ? ItemCount.ToString() + " " : "") + Name;
        }
    }
}
