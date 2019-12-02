using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;
using Website.Areas.Admin.ViewModels;

namespace Website.Areas.Admin.Controllers {
    [Area("Admin")]
    public class PrimeReleaseController : Controller {
        private readonly UnitOfWork _uow;
        private readonly WarframeItemUtilities _itemUtils;
        public PrimeReleaseController(UnitOfWork unitOfWork, WarframeItemUtilities utils) {
            _uow = unitOfWork;
            _itemUtils = utils;
        }

        public IActionResult Index() {
            List<PrimeRelease> primeReleases = _uow.GetRepo<PrimeReleaseRepository>().GetAll();
            primeReleases.ForEach(x => x.LoadItemNames(_uow));
            return View(primeReleases);
        }

        [HttpGet]
        public IActionResult Edit(int? id = null) {
            if (id.HasValue) {
                PrimeRelease pr = _uow.GetRepo<PrimeReleaseRepository>().GetByID(id.Value);
                return View(new EditPrimeReleaseViewModel().Load(_uow, _itemUtils, pr));
            }
            return View(new EditPrimeReleaseViewModel().Load(_uow, _itemUtils, null));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([Bind(Prefix = "PrimeRelease")]PrimeRelease model) {
            if (ModelState.IsValid) {
                if (model.ID == 0) {
                    _uow.GetRepo<PrimeReleaseRepository>().Add(model);
                    TempData["message"] = "New Prime Release Added";
                } else {
                    _uow.GetRepo<PrimeReleaseRepository>().Update(model);
                    TempData["message"] = "Updated Prime Release";
                }
                return RedirectToAction("Index");
            }
            return View(new EditPrimeReleaseViewModel().Load(_uow, _itemUtils, model));
        }
    }
}