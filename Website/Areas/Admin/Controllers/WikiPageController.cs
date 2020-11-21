using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Website.Areas.Admin.Controllers {
    [Area("Admin")]
    public class WikiPageController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
