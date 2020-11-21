using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;

namespace Website.Areas.Admin.Controllers {
    [Area("Admin")]
    public class AmpComponentController : Controller {
        private readonly UnitOfWork uow;

        public AmpComponentController(UnitOfWork uow) {
            this.uow = uow;
        }

        public IActionResult Index() {
            return View(uow.GetRepo<AmpComponentRepository>().GetAll());
        }

        [HttpGet]
        public IActionResult Edit(int? id = null) {
            AmpComponent ampComponent;
            if (id.HasValue) {
                ampComponent = uow.GetRepo<AmpComponentRepository>().GetByID(id.Value);
            } else {
                ampComponent = new AmpComponent();
            }
            return View(ampComponent);
        }

        [HttpPost]
        public IActionResult Edit(AmpComponent model) {
            if (ModelState.IsValid) {
                AmpComponentRepository repo = uow.GetRepo<AmpComponentRepository>();
                if (model.ID == 0) {
                    repo.Add(model);
                } else {
                    repo.Update(model);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
