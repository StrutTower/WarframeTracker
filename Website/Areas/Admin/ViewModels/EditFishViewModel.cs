using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace Website.Areas.Admin.ViewModels {
    public class EditFishViewModel {
        public Fish Fish { get; set; }

        public string SelectedItemName { get; set; }

        public SelectList MapSelectList { get; set; }
        public SelectList RaritySelectList { get; set; }
        public SelectList BiomeSelectList { get; set; }
        public SelectList ActiveTimeSelectList { get; set; }

        public EditFishViewModel Load(WarframeItemUtilities warframeItemUtilities, Fish fish) {
            Fish = fish;
            if (fish != null && !string.IsNullOrWhiteSpace(fish.UniqueName)) {
                var item = warframeItemUtilities.GetByUniqueName(fish.UniqueName);
                SelectedItemName = item.Name;
            }

            string[] maps = new[] { "Plains of Eidolon", "Orb Vallis" };
            MapSelectList = new SelectList(maps);

            string[] rarity = new[] { "Common", "Uncommon", "Rare", "Legendary" };
            RaritySelectList = new SelectList(rarity);

            string[] biomes = new[] { "Ocean", "Lake", "Pond", "Cave", "River", "Pond, River" };
            BiomeSelectList = new SelectList(biomes);

            string[] actity = new[] { "Day and Night", "Day", "Night", "Cold and Ward", "Cold", "Warm" };
            ActiveTimeSelectList = new SelectList(actity);

            return this;
        }
    }
}
