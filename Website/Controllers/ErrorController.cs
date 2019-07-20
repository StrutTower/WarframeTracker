using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers {
    [AllowAnonymous]
    public class ErrorController : Controller {
        public IActionResult Index() {
            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            string errorMessage = string.Empty;
            if (exceptionHandlerPathFeature != null &&
                exceptionHandlerPathFeature.Error != null)
                errorMessage = exceptionHandlerPathFeature.Error.Message;

            return View(model: errorMessage);
        }

        [Route("[controller]/[action]/{statusCode:int}")]
        public IActionResult Code(int statusCode) {
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode) {
                case 401:
                    return RedirectToAction("Login", "Account");
                case 403:
                    return View("Forbidden");
                case 404:
                    return View("404", feature?.OriginalPath);
            }
            throw new NotSupportedException();
        }

        public IActionResult Forbidden() {
            return View();
        }

        public IActionResult NoAccess() {
            return View();
        }
    }
}