using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WarframeTrackerLib.Repository;
using Website.Infrastructure;
using Website.ViewModels;

namespace Website.Controllers {
    public class HomeController : CustomController {
        private readonly AppSettings _appSettings;
        private readonly UnitOfWork _uow;
        public HomeController(IOptions<AppSettings> appSettings, UnitOfWork unitOfWork) {
            _appSettings = appSettings.Value;
            _uow = unitOfWork;
        }

        public IActionResult Index() {
            return View(new HomeViewModel().Load(_uow, User, _appSettings));
        }
    }
}
