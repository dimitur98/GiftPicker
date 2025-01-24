using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftPicker.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorsController : BaseController
    {
        public IActionResult Status(int? code = null)
        {
            return View(code);
        }

        public IActionResult Unknown()
        {
            return this.View();
        }

        public IActionResult NoAccess()
        {
            return this.RedirectToAction(nameof(Status), new {code=403});
        }
    }
}