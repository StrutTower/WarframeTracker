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
    public class InvasionRewardController : Controller {
        private readonly UnitOfWork _uow;
        private readonly WarframeItemUtilities _itemUtils;
        public InvasionRewardController(UnitOfWork unitOfWork, WarframeItemUtilities utils) {
            _uow = unitOfWork;
            _itemUtils = utils;
        }

        public ActionResult Index() {
            List<InvasionReward> rewards = _uow.GetRepo<InvasionRewardRepository>().GetAll();
            return View(rewards);
        }

        [HttpGet]
        public ActionResult Edit(int? id = null) {
            InvasionReward reward = null;
            if (id.HasValue) {
                reward = _uow.GetRepo<InvasionRewardRepository>().GetByID(id.Value);
            }
            return View(new EditInvasionRewardViewModel().Load(_uow, reward));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Prefix = "InvasionReward")]InvasionReward model) {
            if (ModelState.IsValid) {
                if (model.ID == 0) {
                    _uow.GetRepo<InvasionRewardRepository>().Add(model);
                    TempData["message"] = "Invasion Reward Added";
                } else {
                    _uow.GetRepo<InvasionRewardRepository>().Update(model);
                    TempData["message"] = "Invasion Reward Updated";
                }
                return RedirectToAction("Index");
            }
            return View(new EditInvasionRewardViewModel().Load(_uow, model));
        }
    }
}