﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using Website.Areas.Admin.ViewModels;

namespace Website.Areas.Admin.Controllers {
    [Area("Admin")]
    public class ItemCategoryController : Controller {
        private readonly UnitOfWork _uow;
        public ItemCategoryController(UnitOfWork uow) {
            _uow = uow;
        }

        public ActionResult Index() {
            List<ItemCategory> categories = _uow.GetRepo<ItemCategoryRepository>().GetAll();
            new EagerLoader(_uow).Load(categories);
            return View(categories);
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