using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using WarframeTrackerLib.WarframeApi;
using Website.Infrastructure;
using Website.ViewModels;

namespace Website.Controllers {
    [Authorize]
    public class ItemController : CustomController {
        private readonly AppSettings _appSettings;
        public ItemController(IOptions<AppSettings> appSettings) {
            _appSettings = appSettings.Value;
        }

        public ActionResult Index() {
            return View();
        }

        public IActionResult PrimeWishlist() {
            return View(new PrimeWishlistViewModel().Load(User, _appSettings));
        }

        public ActionResult Details(string itemID) {
            itemID = itemID.Replace("|", "/");
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                WarframeItem item = new WarframeItemUtilities(uow).GetByUniqueName(itemID);
                return PartialView("_ItemDetailsModalContent", new ItemDetailsModelViewModel().Load(item, uow));
            }
        }

        public ActionResult Form(string itemID) {
            itemID = itemID.Replace("|", "/");
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                WarframeItem item = new WarframeItemUtilities(uow).GetByUniqueName(itemID);
                ItemCategory itemCategory = new ItemCategoryRepository(uow).GetByID(item.ItemCategoryID);
                new EagerLoader(uow).Load(itemCategory);
                if (itemCategory.CodexTab_Object.CodexSectionID == CodexSection.Relics) {
                    return PartialView("_RelicModal", item);
                }

                return PartialView("_ItemModalContent", new ItemModalViewModel().Load(item, itemCategory, User, uow));
            }
        }

        public ActionResult UpdateItemAcquisition(
            [Bind(Prefix = "ItemAcquisition")]ItemAcquisition model,
            [Bind(Prefix = "ComponentAcquisitions")]List<ComponentAcquisition> compModel) {

            if (ModelState.IsValid) {
                using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                    User user = new UserRepository(uow).GetByUsername(User.Identity.Name);
                    model.UserID = user.ID;

                    WarframeItem item = new WarframeItemUtilities(uow).GetByUniqueName(model.ItemUniqueName);
                    ItemCategory category = new ItemCategoryRepository(uow).GetByID(item.ItemCategoryID);
                    if (!category.CanBeMastered) {
                        model.IsMastered = false;
                    }

                    ItemAcquisitionRepository repo = new ItemAcquisitionRepository(uow);
                    ItemAcquisition existing = repo.GetByPrimaryKeys(user.ID, model.ItemUniqueName);
                    if (existing == null) {
                        repo.Add(model);
                    } else {
                        repo.Update(model);
                    }

                    if (compModel != null && compModel.Any()) {
                        ComponentAcquisitionRepository compRepo = new ComponentAcquisitionRepository(uow);
                        foreach (ComponentAcquisition ca in compModel) {
                            ca.UserID = user.ID;
                            ComponentAcquisition existingComp = compRepo.GetByPrimaryKeys(user.ID, ca.ComponentUniqueName, ca.ItemUniqueName);
                            if (existingComp == null) {
                                compRepo.Add(ca);
                            } else {
                                compRepo.Update(ca);
                            }
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
            return View(new PrimePredictionViewModel().Load(User));
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