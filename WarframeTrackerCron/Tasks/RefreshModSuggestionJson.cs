using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WarframeTrackerLib.Config;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace WarframeTrackerCron.Tasks {
    public class RefreshModSuggestionJson {
        private readonly UnitOfWork uow;
        private readonly WarframeItemUtilities warframeItemUtilities;

        public RefreshModSuggestionJson(UnitOfWork uow, WarframeItemUtilities warframeItemUtilities) {
            this.uow = uow;
            this.warframeItemUtilities = warframeItemUtilities;
        }

        public void StartJob() {
            Run();
        }

        public void Run() {
            List<WarframeItem> allMods = warframeItemUtilities.GetAllMods();

            ModSuggestionRepository repo = uow.GetRepo<ModSuggestionRepository>();
            List<ModSuggestion> modSuggestions = repo.GetAll();

            foreach(ModSuggestion modSuggestion in modSuggestions) {
                WarframeItem item = allMods.SingleOrDefault(x => x.UniqueName == modSuggestion.UniqueName);
                if (item != null) {
                    string json = JsonConvert.SerializeObject(item);
                    if (modSuggestion.WarframeItemJson != json) {
                        modSuggestion.WarframeItemJson = json;
                        repo.Update(modSuggestion);
                    }
                }
            }
        }
    }
}
