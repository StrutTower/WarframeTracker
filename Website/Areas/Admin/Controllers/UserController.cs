using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using Website.Infrastructure;

namespace Website.Areas.Admin.Controllers {
    [Area("Admin")]
    [Authorize, Role(Roles.Administrator)]
    public class UserController : CustomController {
        private readonly UnitOfWork uow;

        public UserController(UnitOfWork uow) {
            this.uow = uow;
        }

        public IActionResult Index() {
            List<User> users = uow.GetRepo<UserRepository>().GetAll();
            return View(users);
        }
    }
}
