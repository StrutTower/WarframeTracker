using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;
using Website.Infrastructure;
using Website.ViewModels;

namespace Website.Controllers {
    public class ToolsController : Controller {
        private readonly AppSettings appSettings;
        private readonly UnitOfWork uow;
        private readonly WarframeItemUtilities itemUtils;

        public ToolsController(IOptions<AppSettings> appSettings, UnitOfWork uow, WarframeItemUtilities itemUtils) {
            this.appSettings = appSettings.Value;
            this.uow = uow;
            this.itemUtils = itemUtils;
        }

        public IActionResult Index() {
            return View();
        }

        [Authorize, HttpGet]
        public IActionResult UpdateDaysLoggedIn() {
            int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            UserData userData = uow.GetRepo<UserDataRepository>().GetByUserID(userID);
            return View(userData);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UpdateDaysLoggedIn(int? daysLoggedIn) {
            int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            UserDataRepository repo = uow.GetRepo<UserDataRepository>();
            UserData userData = repo.GetByUserID(userID);
            userData.DaysLoggedIn = daysLoggedIn;
            userData.DaysLoggedInUtcStartDate = DateTime.UtcNow.Date;
            repo.Update(userData);
            TempData["message"] = "Days logged in updated";
            return RedirectToAction("Index");
        }

        public IActionResult PrimePrediction() {
            return View(new PrimePredictionViewModel().Load(uow, itemUtils, appSettings));
        }

        public IActionResult AmpInfo() {
            return View(new AmpInfoViewModel().Load(uow));
        }
    }
}