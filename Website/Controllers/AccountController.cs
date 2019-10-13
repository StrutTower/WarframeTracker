using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        //TODO Add Account Management
        [Authorize]
        public IActionResult Management() {
            return View();
        }

        //TODO Add Registration
        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(RegistrationViewModel model) {

            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {
            if (ModelState.IsValid) {
                User user;
                using (UnitOfWork uow = UnitOfWork.CreateNew()) {
                    user = new UserRepository(uow).GetByUsername(model.UserName);
                }

                if (user != null) {
                    if (new Hasher().Validate(user.Password, model.Password, user.Salt)) {
                        List<Claim> claims = new List<Claim> {
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(ClaimTypes.Email, user.EmailAddress),
                            new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
                        };

                        if (user.Username.Equals("StrutTower", StringComparison.InvariantCultureIgnoreCase)) {
                            claims.Add(new Claim(ClaimTypes.Role, RoleTypes.Administrator));
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
    }
}