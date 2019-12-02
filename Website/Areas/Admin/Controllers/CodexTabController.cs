using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;

namespace Website.Areas.Admin.Controllers {
    [Area("Admin")]
    public class CodexTabController : Controller {
        private readonly UnitOfWork _uow;
        public CodexTabController(UnitOfWork uow) {
            _uow = uow;
        }

        public ActionResult Index() {
            List<CodexTab> tabs = _uow.GetRepo<CodexTabRepository>().GetAll();
            return View(tabs);
        }

        [HttpGet]
        public ActionResult Edit(int? id = null) {
            if (id.HasValue) {
                CodexTab tab = _uow.GetRepo<CodexTabRepository>().GetByID(id.Value);
                return View(tab);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(CodexTab model) {
            if (ModelState.IsValid) {
                if (model.ID == 0) {
                    _uow.GetRepo<CodexTabRepository>().Add(model);
                    TempData["message"] = "Added New Codex Tab";
                } else {
                    _uow.GetRepo<CodexTabRepository>().Update(model);
                    TempData["message"] = "Updated Codex Tab";
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}