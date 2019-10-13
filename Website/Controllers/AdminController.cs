using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TowerSoft.Repository;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using WarframeTrackerLib.WarframeApi;
using Website.Infrastructure;
using Website.ViewModels;

namespace Website.Controllers {
    [Role(RoleTypes.Administrator)]
    public class AdminController : CustomController {
        public ActionResult Index() {
            return View();
        }

        public ActionResult RedownloadCache() {
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                new WarframeItemUtilities(uow).RedownloadCache();
            }
            TempData["message"] = "Image Cache Redownloaded.";
            return RedirectToAction("Index");
        }

        #region CodexTabs
        public ActionResult ManageCodexTabs() {
            List<CodexTab> tabs;
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                tabs = new CodexTabRepository(uow).GetAll();
            }
            return View(tabs);
        }

        [HttpGet]
        public ActionResult EditCodexTab(int? id = null) {
            if (id.HasValue) {
                CodexTab tab;
                using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                    tab = new CodexTabRepository(uow).GetByID(id.Value);
                }
                return View(tab);
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditCodexTab(CodexTab model) {
            if (ModelState.IsValid) {
                using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                    CodexTabRepository repo = new CodexTabRepository(uow);
                    if (model.ID == 0) {
                        repo.Add(model);
                        TempData["message"] = "Added New Codex Tab";
                    } else {
                        repo.Update(model);
                        TempData["message"] = "Updated Codex Tab";
                    }
                }
                return RedirectToAction("ManageCodexTabs");
            }
            return View(model);
        }
        #endregion

        #region ItemCategories
        public ActionResult ManageItemCategories() {
            List<ItemCategory> categories;
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                categories = new ItemCategoryRepository(uow).GetAll();
                new EagerLoader(uow).Load(categories);
            }
            return View(categories);
        }

        [HttpGet]
        public ActionResult EditItemCategory(int? id = null) {
            EditItemCategoryViewModel viewmodel;
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                if (id.HasValue) {
                    ItemCategory cat;
                    cat = new ItemCategoryRepository(uow).GetByID(id.Value);
                    viewmodel = new EditItemCategoryViewModel(uow, cat);
                } else {
                    viewmodel = new EditItemCategoryViewModel(uow, null);
                }
            }
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult EditItemCategory([Bind(Prefix = "ItemCategory")]ItemCategory model) {
            EditItemCategoryViewModel viewmodel;
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                if (ModelState.IsValid) {
                    ItemCategoryRepository repo = new ItemCategoryRepository(uow);
                    if (model.ID == 0) {
                        repo.Add(model);
                        TempData["message"] = "Added Item Category";
                    } else {
                        repo.Update(model);
                        TempData["message"] = "Updated Item Category";
                    }
                    return RedirectToAction("ManageItemCategories");
                }
                viewmodel = new EditItemCategoryViewModel(uow, model);
            }
            return View(viewmodel);
        }
        #endregion

        #region ManualItemData
        public ActionResult ManageManualItemData() {
            List<ManualItemData> data;
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                data = new ManualItemDataRepository(uow).GetAll();
            }
            return View(data);
        }

        [HttpGet]
        public ActionResult EditManualItemData(int? id = null, bool addAnother = false) {
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                if (id.HasValue) {
                    ManualItemData data = new ManualItemDataRepository(uow).GetByID(id.Value);
                    return View(new EditManualItemDataViewModel().Load(data, uow));
                }
                return View(new EditManualItemDataViewModel().Load(null, uow));
            }
        }

        [HttpPost]
        public ActionResult EditManualItemData([Bind(Prefix = "ManualItemData")]ManualItemData model, bool addAnother) {
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                if (ModelState.IsValid) {
                    ManualItemDataRepository repo = new ManualItemDataRepository(uow);
                    if (model.ID == 0) {
                        repo.Add(model);
                        TempData["message"] = "Added new manual item data";
                    } else {
                        repo.Update(model);
                        TempData["message"] = "Updated manual item data";
                    }
                    if (addAnother) return RedirectToAction("EditManualItemData", new { addAnother });
                    else return RedirectToAction("ManageManualItemData");
                }
                return View(new EditManualItemDataViewModel().Load(model, uow));
            }
        }
        #endregion

        #region InvasionRewards
        public ActionResult ManageInvasionRewards() {
            List<InvasionReward> rewards;
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                rewards = new InvasionRewardRepository(uow).GetAll();
            }
            return View(rewards);
        }

        [HttpGet]
        public ActionResult EditInvasionReward(int? id = null) {
            InvasionReward reward = null;
            if (id.HasValue) {
                using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                    reward = new InvasionRewardRepository(uow).GetByID(id.Value);
                }
            }
            return View(new EditInvasionRewardViewModel().Load(reward));
        }

        [HttpPost]
        public ActionResult EditInvasionReward([Bind(Prefix = "InvasionReward")]InvasionReward model) {
            if (ModelState.IsValid) {
                using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                    InvasionRewardRepository repo = new InvasionRewardRepository(uow);
                    if (model.ID == 0) {
                        repo.Add(model);
                        TempData["message"] = "Invasion Reward Added";
                    } else {
                        repo.Update(model);
                        TempData["message"] = "Invasion Reward Updated";
                    }
                }
                return RedirectToAction("ManageInvasionRewards");
            }
            return View(new EditInvasionRewardViewModel().Load(model));
        }
        #endregion

        #region PrimeReleases
        public IActionResult ManagePrimeReleases() {
            List<PrimeRelease> primeReleases;
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                primeReleases = new PrimeReleaseRepository(uow).GetAll();
                primeReleases.ForEach(x => x.LoadItemNames(uow));
            }
            return View(primeReleases);
        }

        [HttpGet]
        public IActionResult EditPrimeRelease(int? id = null) {
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                if (id.HasValue) {
                    PrimeRelease pr = new PrimeReleaseRepository(uow).GetByID(id.Value);
                    return View(new EditPrimeReleaseViewModel().Load(uow, pr));
                }
                return View(new EditPrimeReleaseViewModel().Load(uow, null));
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditPrimeRelease([Bind(Prefix = "PrimeRelease")]PrimeRelease model) {
            using (IUnitOfWork uow = new UnitOfWorkFactory().UnitOfWork) {
                if (ModelState.IsValid) {
                    PrimeReleaseRepository repo = new PrimeReleaseRepository(uow);
                    if (model.ID == 0) {
                        repo.Add(model);
                        TempData["message"] = "New Prime Release Added";
                    } else {
                        repo.Update(model);
                        TempData["message"] = "Updated Prime Release";
                    }
                    return RedirectToAction("ManagePrimeReleases");
                }
                return View(new EditPrimeReleaseViewModel().Load(uow, model));
            }
        }
        #endregion
    }
}