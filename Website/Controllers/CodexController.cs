using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;
using Website.Infrastructure;
using Website.ViewModels;

namespace Website.Controllers {
    public class CodexController : CustomController {
        private readonly UnitOfWork _uow;
        private readonly WarframeItemUtilities _itemUtils;
        public CodexController(UnitOfWork unitOfWork, WarframeItemUtilities warframeItemUtilities) {
            _uow = unitOfWork;
            _itemUtils = warframeItemUtilities;
        }

        public ActionResult Index() {
            return View();
        }

        public IActionResult View(CodexSection id) {
            CodexViewModel viewmodel = new CodexViewModel().Load(_uow, _itemUtils, User, id);
            return View(viewmodel);
        }

        public IActionResult ViewTab(CodexSection id, int tab) {
            return View("View", new CodexViewModel().Load(_uow, _itemUtils, User, id, tab));
        }
    }
}