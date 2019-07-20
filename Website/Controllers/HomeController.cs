using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using Website.Infrastructure;
using Website.ViewModels;

namespace Website.Controllers {
    public class HomeController : CustomController {
        private readonly AppSettings _appSettings;
        public HomeController(IOptions<AppSettings> appSettings) {
            _appSettings = appSettings.Value;
        }

        [Authorize]
        public IActionResult Index() {
            return View(new HomeViewModel().Load(User, _appSettings));
        }

        [Authorize, HttpGet]
        public ActionResult UpdateStarchartCompletion() {
            int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                UserData userData = new UserDataRepository(uow).GetByUserID(userID);
                return View(userData);
            }
        }

        [Authorize, HttpPost]
        public ActionResult UpdateStarchartCompletion(UserData model) {
            if (ModelState.IsValid) {
                using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                    int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    UserDataRepository repo = new UserDataRepository(uow);
                    UserData userData = repo.GetByUserID(userID);
                    userData.JunctionsCompleted = model.JunctionsCompleted;
                    userData.MissionNodesCompleted = model.MissionNodesCompleted;
                    repo.Update(userData);
                }
                TempData["message"] = "Updated";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult ReleaseNotes() {
            return View();
        }
    }
}
