using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WarframeTrackerLib.Domain;
using WarframeTrackerLib.Repository;
using WarframeTrackerLib.Utilities;
using Website.Infrastructure;
using Website.ViewModels;

namespace Website.Controllers {
    public class AccountController : CustomController {
        private readonly UnitOfWork uow;
        private readonly AppSettings appSettings;
        public AccountController(UnitOfWork uow, IOptions<AppSettings> appSettings) {
            this.uow = uow;
            this.appSettings = appSettings.Value;
        }

        //TODO Add Account Management
        [Authorize]
        public IActionResult Management() {
            return View();
        }

        #region Register
        [HttpGet]
        public IActionResult Register() {
            if (appSettings.AllowRegistration) {
                return View(new RegistrationViewModel());
            }
            return Message("Registration is currently disabled on this site");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(RegistrationViewModel model) {
            if (appSettings.AllowRegistration) {
                if (ModelState.IsValid) {
                    var saltHash = new HashUtilities().CreateSaltAndHash(model.Password);
                    User user = new User {
                        Username = model.Username,
                        EmailAddress = model.EmailAddress,
                        Password = saltHash.Value,
                        Salt = saltHash.Key
                    };
                    uow.GetRepo<UserRepository>().Add(user);
                    TempData["message"] = "Account Registered";
                    return RedirectToAction("Login");
                }
                return View(model);
            }
            return Message("Registration is currently disabled on this site");
        }
        #endregion

        #region Login/Logout
        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {
            if (ModelState.IsValid) {
                User user;
                user = uow.GetRepo<UserRepository>().GetByUsername(model.UserName);

                if (user != null) {
                    if (new HashUtilities().Validate(model.Password, user.Password, user.Salt)) {
                        List<Claim> claims = new List<Claim> {
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(ClaimTypes.Email, user.EmailAddress),
                            new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
                        };

                        if (user.Username.Equals("StrutTower", StringComparison.InvariantCultureIgnoreCase)) {
                            claims.Add(new Claim(ClaimTypes.Role, Roles.Administrator));
                        }

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties authProps = new AuthenticationProperties {
                            IsPersistent = true,
                            AllowRefresh = true
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(identity),
                            authProps);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("Password", "The username or password are incorrect.");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        #endregion
    }
}