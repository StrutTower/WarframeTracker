using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarframeTrackerLib.Domain;
using Website.Infrastructure;
using Website.ViewModels;

namespace Website.Controllers {
    public class CodexController : CustomController {
        public ActionResult Index() {
            return View();
        }

        public IActionResult View(CodexSection id) {
            CodexViewModel viewmodel = new CodexViewModel().Load(User, id);
            return View(viewmodel);
        }

        public IActionResult ViewTab(CodexSection id, int tab) {
            return View("View", new CodexViewModel().Load(User, id, tab));
        }
    }
}