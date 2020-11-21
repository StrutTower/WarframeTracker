using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace Website.Areas.Admin.ViewModels {
    public class EditModSuggestionViewModel {
        public ModSuggestion ModSuggestion { get; set; }

        public SelectList ModSuggestionList { get; set; }

        public SelectList ModList { get; set; }

        public EditModSuggestionViewModel Load(ModSuggestion modSuggestion, UnitOfWork uow, WarframeItemUtilities warframeItemUtilities) {
            ModSuggestion = modSuggestion;

            List<ModSuggestion> mods = uow.GetRepo<ModSuggestionRepository>().GetAll().Where(x => x.ModCategory == ModSuggestion.ModCategory).ToList();
            if (ModSuggestion != null) {
                mods = mods.Where(x => x.ID != ModSuggestion.ID).ToList();
            }
            ModSuggestionList = new SelectList(mods, "ID", "WarframeItem.Name");

            List<WarframeItem> allMods = warframeItemUtilities.GetAllMods().Where(x => !x.UniqueName.EndsWith("Beginner")).ToList();
            ModList = new SelectList(allMods, "UniqueName", "Name");
            return this;
        }
    }
}
