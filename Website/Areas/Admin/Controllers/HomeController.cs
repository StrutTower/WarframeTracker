using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;

namespace Website.Areas.Admin.Controllers {
    [Area("Admin")]
    public class HomeController : Controller {
        private readonly UnitOfWork _uow;
        private readonly WarframeItemUtilities _itemUtils;
        public HomeController(UnitOfWork unitOfWork, WarframeItemUtilities utils) {
            _uow = unitOfWork;
            _itemUtils = utils;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult RedownloadCache() {
            _itemUtils.RedownloadCache();
            TempData["message"] = "Image Cache Redownloaded.";
            return RedirectToAction("Index");
        }

        public IActionResult ApiExplorer() {
            return View();
        }
    }
}