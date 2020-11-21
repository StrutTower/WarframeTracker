using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using Website.Areas.Admin.ViewModels;
using Website.Infrastructure;

namespace Website.Areas.Admin.Controllers {
    [Area("Admin")]
    [Authorize, Role(Roles.Administrator)]
    public class ItemCategoryController : CustomController {
        private readonly UnitOfWork _uow;
        public ItemCategoryController(UnitOfWork uow) {
            _uow = uow;
        }

        public ActionResult Index() {
            //List<ItemCategory> categories = _uow.GetRepo<ItemCategoryRepository>().GetAll();
            //new EagerLoader(_uow).Load(categories);
            List<CodexTab> tabs = _uow.GetRepo<CodexTabRepository>().GetAll();
            new EagerLoader(_uow).Load(tabs);
            return View(tabs);
        }

        [HttpGet]
        public ActionResult Edit(int? id = null) {
            EditItemCategoryViewModel viewmodel;
            if (id.HasValue) {
                ItemCategory cat = _uow.GetRepo<ItemCategoryRepository>().GetByID(id.Value);
                viewmodel = new EditItemCategoryViewModel(_uow, cat);
            } else {
                viewmodel = new EditItemCategoryViewModel(_uow, null);
            }
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Prefix = "ItemCategory")]ItemCategory model) {
            EditItemCategoryViewModel viewmodel;
            if (ModelState.IsValid) {
                if (model.ID == 0) {
                    _uow.GetRepo<ItemCategoryRepository>().Add(model);
                    TempData["message"] = "Added Item Category";
                } else {
                    _uow.GetRepo<ItemCategoryRepository>().Update(model);
                    TempData["message"] = "Updated Item Category";
                }
                return RedirectToAction("Index");
            }
            viewmodel = new EditItemCategoryViewModel(_uow, model);
            return View(viewmodel);
        }
    }
}