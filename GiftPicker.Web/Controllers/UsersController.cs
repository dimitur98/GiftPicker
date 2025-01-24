using GiftPicker.Db;
using GiftPicker.Db.Models.Search.Users;
using GiftPicker.Web.Resources;
using GiftPicker.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GiftPicker.Web.Controllers
{  
    public class UsersController : BaseController
    {
        public IActionResult Search()
        {
            var model = new UsersSearchModel();
            {
                model.Response = GiftPickerDb.Users.Search(new Request());
            }

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public IActionResult Login(string returnUrl)
        {
            if (this.LoggedUser != null)
            {
                return this.RedirectToAction(string.Empty, string.Empty);
            }

            this.ViewBag.ReturnUrl = returnUrl;

            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (this.LoggedUser != null) { return this.RedirectToAction(string.Empty, string.Empty); }

            if (this.ModelState.IsValid)
            {
                var user = GiftPickerDb.Users.GetByUsernameAndPassword(model.Username, model.Password);

                if (user == null) { this.ModelState.AddModelError(string.Empty, Global.IncorrectCredentials); }

                if (user != null && this.ModelState.IsValid)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return this.RedirectToAction(nameof(Search));
                }
            }

            return this.View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> LogOff()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(string.Empty, string.Empty);
        }
    }
}