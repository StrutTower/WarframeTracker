using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using WarframeTrackerLib.Config;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using WarframeTrackerLib.WarframeApi;
using Website.Infrastructure;
using Website.ViewModels;

namespace Website.Controllers {
    public class HomeController : CustomController {
        private readonly AppSettings _appSettings;
        private readonly UnitOfWork _uow;
        private readonly TwitterHelper twitterHelper;

        public HomeController(IOptions<AppSettings> appSettings, UnitOfWork unitOfWork, TwitterHelper twitterHelper) {
            _appSettings = appSettings.Value;
            _uow = unitOfWork;
            this.twitterHelper = twitterHelper;
        }

        public IActionResult Index() {
            //twitterHelper.SendTweet();
            return View(new HomeViewModel().Load(_uow, User, _appSettings));
        }
    }
}
