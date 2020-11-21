using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using Website.Infrastructure;

namespace Website.Controllers {
    public class GuidesController : CustomController {
        private readonly UnitOfWork uow;

        public GuidesController(UnitOfWork uow) {
            this.uow = uow;
        }

        public IActionResult Index() {
            WikiPage indexPage = uow.GetRepo<WikiPageRepository>().GetIndexPageByType(WikiPageType.Guides);
            if (indexPage == null && User.IsInRole(Roles.Administrator)) {
                return Message("Not Implemented Yet");
            } else if (indexPage == null) {
                return Message("This page has not been created yet.");
            }
            return View(indexPage);
        }

        public IActionResult ModSuggestions() {
            List<ModSuggestion> mods = uow.GetRepo<ModSuggestionRepository>().GetAll();
            List<ModSuggestion> firstLevel = mods.Where(x => !x.PredecessorModSuggestionID.HasValue).ToList();
            foreach (var mod in firstLevel) {
                mod.UpgradeMod_Objects = mods.Where(x => x.PredecessorModSuggestionID == mod.ID).ToList();
            }
            return View(firstLevel);
        }
    }
}
