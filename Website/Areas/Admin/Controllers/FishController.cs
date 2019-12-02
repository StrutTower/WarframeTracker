using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using WarframeTrackerLib.WarframeApi;
using Website.Areas.Admin.ViewModels;

namespace Website.Areas.Admin.Controllers {
    [Area("Admin")]
    public class FishController : Controller {
        private readonly UnitOfWork _uow;
        private readonly WarframeItemUtilities _warframeItemUtilities;
        public FishController(UnitOfWork uow, WarframeItemUtilities warframeItemUtilities) {
            _uow = uow;
            _warframeItemUtilities = warframeItemUtilities;
        }

        public IActionResult Index() {
            List<Fish> fish = _uow.GetRepo<FishRepository>().GetAll();
            new EagerLoader(_uow).Load(fish);
            return View(fish);
        }

        [HttpGet]
        public IActionResult Edit(int? id = null) {
            Fish fish = null;
            if (id.HasValue) {
                fish = _uow.GetRepo<FishRepository>().GetByID(id.Value);
            }
            return View(new EditFishViewModel().Load(_warframeItemUtilities, fish));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([Bind(Prefix = "Fish")]Fish fish) {
            if (ModelState.IsValid) {
                if (fish.ID == 0) {
                    _uow.GetRepo<FishRepository>().Add(fish);
                    TempData["message"] = "Added New Fish";
                } else {
                    _uow.GetRepo<FishRepository>().Update(fish);
                    TempData["message"] = "Updated Fish";
                }
                return RedirectToAction("Index");
            }
            return View(fish);
        }
    }
}