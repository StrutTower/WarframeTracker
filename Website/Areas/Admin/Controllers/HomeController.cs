using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;
using Website.Areas.Admin.ViewModels;
using Website.Infrastructure;

namespace Website.Areas.Admin.Controllers {
    [Area("Admin")]
    [Authorize, Role(Roles.Administrator)]
    public class HomeController : CustomController {
        private readonly UnitOfWork uow;
        private readonly WarframeItemUtilities itemUtils;
        private readonly AppSettings appSettings;
        public HomeController(UnitOfWork uow, WarframeItemUtilities itemUtils, IOptions<AppSettings> appSettings) {
            this.uow = uow;
            this.itemUtils = itemUtils;
            this.appSettings = appSettings.Value;
        }

        public IActionResult Index() {
            return View(new AdminIndexViewModel().Load(uow, itemUtils, appSettings));
        }

        public IActionResult RedownloadCache() {
            itemUtils.RedownloadCache();
            TempData["message"] = "Image Cache Redownloaded.";
            return RedirectToAction("Index");
        }

        public IActionResult ApiExplorer() {
            return View();
        }

        public IActionResult ModCompatTypes() {
            List<WarframeItem> mods = itemUtils.GetAllMods();
            ViewData["augments"] = mods.Where(x => x.IsAugment == true).ToList();
            List<IGrouping<string, WarframeItem>> grouped = mods.Where(x => x.IsAugment != true).GroupBy(x => x.CompatName).ToList();
            return View(grouped);
        }

        public IActionResult ModPlaceholders() {
            Regex regex = new Regex(@"<.*?>");
            List<string> placeholders = new List<string>();
            List<WarframeItem> mods = itemUtils.GetByCodexSection(CodexSection.Mods);
            foreach (WarframeItem mod in mods) {
                if (mod.LevelStats != null && mod.LevelStats.Any()) {
                    foreach (string stat in mod.LevelStats.Last().Stats) {
                        if (regex.IsMatch(stat)) {
                            Match match = regex.Match(stat);
                            if (match.Value.Contains("<LINE_SEPARATOR>")) {
                                Console.WriteLine();
                            }
                            if (!placeholders.Contains(match.Value)) {
                                placeholders.Add(match.Value);
                            }
                        }
                    }
                }
            }
            placeholders = placeholders.Distinct().ToList();
            return View(placeholders);
        }

        public IActionResult RefreshModSuggestions() {
            List<WarframeItem> allMods = itemUtils.GetAllMods();

            ModSuggestionRepository repo = uow.GetRepo<ModSuggestionRepository>();
            List<ModSuggestion> modSuggestions = repo.GetAll();

            foreach (ModSuggestion modSuggestion in modSuggestions) {
                WarframeItem item = allMods.SingleOrDefault(x => x.UniqueName == modSuggestion.UniqueName);
                if (item != null) {
                    string json = JsonConvert.SerializeObject(item);
                    if (modSuggestion.WarframeItemJson != json) {
                        modSuggestion.WarframeItemJson = json;
                        repo.Update(modSuggestion);
                    }
                }
            }
            return Message("Done");
        }
    }
}