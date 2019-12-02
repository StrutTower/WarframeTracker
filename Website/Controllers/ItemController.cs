using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using WarframeTrackerLib.WarframeApi;
using Website.Infrastructure;
using Website.ViewModels;

namespace Website.Controllers {
    public class ItemController : CustomController {
        private readonly AppSettings _appSettings;
        private readonly UnitOfWork _uow;
        private readonly WarframeItemUtilities _itemUtils;
        public ItemController(IOptions<AppSettings> appSettings, UnitOfWork unitOfWork, WarframeItemUtilities utils) {
            _appSettings = appSettings.Value;
            _uow = unitOfWork;
            _itemUtils = utils;
        }

        public ActionResult Index() {
            return View();
        }

        public IActionResult PrimeWishlist() {
            return View(new PrimeWishlistViewModel().Load(_uow, _itemUtils, User, _appSettings));
        }

        public ActionResult Form(string itemID) {
            itemID = itemID.Replace("|", "/");
            WarframeItem item = _itemUtils.GetByUniqueName(itemID);
            ItemCategory itemCategory = _uow.GetRepo<ItemCategoryRepository>().GetByID(item.ItemCategoryID);
            new EagerLoader(_uow).Load(itemCategory);
            if (itemCategory.CodexTab_Object.CodexSectionID == CodexSection.Relics) {
                return PartialView("_RelicModal", item);
            }

            return PartialView("_ItemModalContent", new ItemModalViewModel().Load(item, itemCategory, User, _uow, _itemUtils));
        }

        [Authorize]
        public ActionResult UpdateItemAcquisition(
            [Bind(Prefix = "ItemAcquisition")]ItemAcquisition model,
            [Bind(Prefix = "ComponentAcquisitions")]List<ComponentAcquisition> compModel) {

            if (ModelState.IsValid) {
                User user = _uow.GetRepo<UserRepository>().GetByUsername(User.Identity.Name);
                model.UserID = user.ID;

                WarframeItem item = _itemUtils.GetByUniqueName(model.ItemUniqueName);
                ItemCategory category = _uow.GetRepo<ItemCategoryRepository>().GetByID(item.ItemCategoryID);
                if (!category.CanBeMastered) {
                    model.IsMastered = false;
                }

                ItemAcquisition existing = _uow.GetRepo<ItemAcquisitionRepository>().GetByPrimaryKeys(user.ID, model.ItemUniqueName);
                if (existing == null) {
                    _uow.GetRepo<ItemAcquisitionRepository>().Add(model);
                } else {
                    _uow.GetRepo<ItemAcquisitionRepository>().Update(model);
                }

                if (compModel != null && compModel.Any()) {
                    foreach (ComponentAcquisition ca in compModel) {
                        ca.UserID = user.ID;
                        ComponentAcquisition existingComp = _uow.GetRepo<ComponentAcquisitionRepository>().GetByPrimaryKeys(user.ID, ca.ComponentUniqueName, ca.ItemUniqueName);
                        if (existingComp == null) {
                            _uow.GetRepo<ComponentAcquisitionRepository>().Add(ca);
                        } else {
                            _uow.GetRepo<ComponentAcquisitionRepository>().Update(ca);
                        }
                    }
                }

                return Json(new {
                    success = true,
                    itemName = model.ItemUniqueName,
                    view = RenderViewAsync("AcquisitionIcon", model)
                });
            }
            return Json(new {
                success = false
            });
        }


        public IActionResult PrimePrediction() {
            return View(new PrimePredictionViewModel().Load(_uow, _itemUtils, _appSettings));
        }

        public IActionResult ModularItemInfo() {
            return View();
        }

        public IActionResult Zaws() {
            return View();
        }

        public IActionResult Kitguns() {
            return View();
        }

        public IActionResult Amps() {
            return View();
        }
    }
}