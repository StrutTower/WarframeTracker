using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.WarframeApi;
using Website.Infrastructure;
using Website.ViewModels;

namespace Website.Controllers {
    public class SearchController : CustomController {
        private readonly UnitOfWork _uow;
        private readonly WarframeItemUtilities _itemUtils;
        public SearchController(UnitOfWork unitOfWork, WarframeItemUtilities itemUtils) {
            _uow = unitOfWork;
            _itemUtils = itemUtils;
        }

        public IActionResult Index() {
            return View(new SearchViewModel().Load(_uow));
        }

        public IActionResult Results(SearchViewModel model) {
            if (ModelState.IsValid) {
                return View("Items", new SearchResultsViewModel().Load(_uow, _itemUtils, model.AdvancedSearchModel));
            }
            return View("Index", model.Load(_uow));
        }

        public ActionResult Basic(string q) {
            List<WarframeItem> items = _itemUtils.GetAll();
            var items2 = items.Where(x => x.Name.ToLower().Contains(q.ToLower())).ToList();
            return View("Items", items2);
        }

        public ActionResult WarframeItemsAjax(string q, int? page = null) {
            int itemsPerPage = 15;
            List<WarframeItem> items = _itemUtils.Search(q).OrderBy(x => x.Name).ToList();
            bool moreResults = false;
            if (page.HasValue) {
                List<WarframeItem> skippedList = items.Skip((page.Value - 1) * itemsPerPage).ToList();
                items = skippedList.Take(itemsPerPage).ToList();
                if (skippedList.Count > items.Count) {
                    moreResults = true;
                }
            }
            List<object> results = new List<object>();
            foreach (WarframeItem item in items) {
                results.Add(new {
                    id = item.UniqueName,
                    text = item.Name
                });
            }
            if (page.HasValue) {
                return Json(new {
                    results,
                    pagination = new {
                        more = moreResults
                    }
                });
            }
            return Json(new {
                results
            });
        }
    }
}