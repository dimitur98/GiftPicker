using GiftPicker.Db;
using GiftPicker.Db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GiftPicker.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private User _loggedUser;

        public User LoggedUser
        {
            get
            {
                if (_loggedUser == null)
                {
                    var username = this.User?.Identity?.Name;

                    if (!string.IsNullOrEmpty(username)) { _loggedUser = GiftPickerDb.Users.GetByUsername(username); }
                }

                return _loggedUser;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            this.ViewBag.LoggedUser = this.LoggedUser;
        }
    }
}
