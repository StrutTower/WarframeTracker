using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;
using Website.Areas.Admin.ViewModels;
using Website.Infrastructure;

namespace Website.Areas.Admin.Controllers {
    [Area("Admin")]
    public class ModSuggestionController : CustomController {
        private readonly UnitOfWork uow;
        private readonly WarframeItemUtilities warframeItemUtilities;

        public ModSuggestionController(UnitOfWork uow, WarframeItemUtilities warframeItemUtilities) {
            this.uow = uow;
            this.warframeItemUtilities = warframeItemUtilities;
        }

        public IActionResult Index() {
            List<ModSuggestion> modSuggestions = uow.GetRepo<ModSuggestionRepository>().GetAll();
            return View(modSuggestions);
        }

        [HttpGet]
        public IActionResult Edit(int? id = null, ModCategory? modCategory = null) {
            if (id.HasValue) {
                ModSuggestion mod = uow.GetRepo<ModSuggestionRepository>().GetByID(id.Value);
                return View(new EditModSuggestionViewModel().Load(mod, uow, warframeItemUtilities));
            }
            if (modCategory.HasValue) {
                ModSuggestion mod = new ModSuggestion {
                    ModCategory = modCategory.Value
                };
                return View(new EditModSuggestionViewModel().Load(mod, uow, warframeItemUtilities));
            }
            return Message("Missing Parameters");
        }

        [HttpPost]
        public IActionResult Edit([Bind(Prefix = "ModSuggestion")]ModSuggestion model) {
            if (ModelState.IsValid) {
                ModSuggestionRepository repo = uow.GetRepo<ModSuggestionRepository>();
                WarframeItem warframeItem = warframeItemUtilities.GetAllMods().SingleOrDefault(x => x.UniqueName == model.UniqueName);
                warframeItem.Name = warframeItem.Name.Replace("'S", "'s");
                model.WarframeItemJson = JsonConvert.SerializeObject(warframeItem);
                if (model.ID == 0) {
                    repo.Add(model);
                    TempData["message"] = "Mod Suggestion Added";
                } else {
                    repo.Update(model);
                    TempData["message"] = "Mod Suggestion Updated";
                }
                return RedirectToAction("Index");
            }
            return View(new EditModSuggestionViewModel().Load(model, uow, warframeItemUtilities));
        }
    }
}
