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
    public class ManualItemDataController : Controller {
        private readonly UnitOfWork _uow;
        private readonly WarframeItemUtilities _itemUtils;
        public ManualItemDataController(UnitOfWork unitOfWork, WarframeItemUtilities utils) {
            _uow = unitOfWork;
            _itemUtils = utils;
        }

        public ActionResult Index() {
            return View(new ManualItemDataViewModel().Load(_uow, _itemUtils));
        }

        [HttpGet]
        public ActionResult Edit(int? id = null, bool addAnother = false) {
            if (id.HasValue) {
                ManualItemData data = _uow.GetRepo<ManualItemDataRepository>().GetByID(id.Value);
                return View(new EditManualItemDataViewModel().Load(data, _uow, _itemUtils));
            }
            return View(new EditManualItemDataViewModel().Load(null, _uow, _itemUtils));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Prefix = "ManualItemData")]ManualItemData model, bool addAnother) {
            if (ModelState.IsValid) {
                if (model.ID == 0) {
                    _uow.GetRepo<ManualItemDataRepository>().Add(model);
                    TempData["message"] = "Added new manual item data";
                } else {
                    _uow.GetRepo<ManualItemDataRepository>().Update(model);
                    TempData["message"] = "Updated manual item data";
                }
                if (addAnother) return RedirectToAction("EditManualItemData", new { addAnother });
                else return RedirectToAction("Index");
            }
            return View(new EditManualItemDataViewModel().Load(model, _uow, _itemUtils));
        }
    }
}