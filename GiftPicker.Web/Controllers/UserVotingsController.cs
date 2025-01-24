using GiftPicker.Db;
using GiftPicker.Db.Models;
using GiftPicker.Db.Models.Search.UserVotings;
using GiftPicker.Web.Models.Enums;
using GiftPicker.Web.Resources;
using GiftPicker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GiftPicker.Web.Controllers
{
    public class UserVotingsController : BaseController
    {
        public IActionResult Search()
        {
            var request = new Request
            {
                ExcludedUserId = this.LoggedUser.Id,
                SortColumn = "uv.year, MONTH(u.birthday), DAY(u.birthday)",
            };

            var model = new UserVotingSearchModel()
            {
                Response = GiftPickerDb.UserVotings.Search(request),
                VotedUserVotingIds = GiftPickerDb.UserVotings.GetVotedIds(this.LoggedUser.Id)
            };

            if (model.Response.Records?.Any() != null)
            {
                GiftPickerDb.UserVotings.LoadUsers(model.Response.Records);
                GiftPickerDb.UserVotings.LoadCreators(model.Response.Records);
            }

            return this.View(model);
        }

        public IActionResult Create(uint? userId)
        {
            var model = new UserVotingCreateModel 
            {
                UserId = userId
            };

            model.LoadBase(this.LoggedUser.Id);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Create(UserVotingCreateModel model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    if (GiftPickerDb.UserVotings.Exists(model.UserId.Value, model.Year.Value))
                    {
                        this.ModelState.AddModelError(nameof(model.UserId), string.Format(Global.UserVotingExists, model.Year));
                    }

                    if (model.UserId == this.LoggedUser.Id)
                    {
                        this.ModelState.AddModelError(nameof(model.UserId), Global.UserVotingSelf);
                    }

                    var user = GiftPickerDb.Users.GetById(model.UserId.Value);

                    if (DateTime.Now.Year == model.Year.Value && DateTime.Now.Month >= user.Birthday.Month && DateTime.Now.Date > user.Birthday.Date)
                    {
                        this.ModelState.AddModelError(string.Empty, Global.PassedBirthday);
                    }
                }

                if (this.ModelState.IsValid)
                {
                    var userVoting = new UserVoting
                    {
                        UserId = model.UserId.Value,
                        CreatorId = this.LoggedUser.Id,
                        Year = model.Year.Value
                    };

                    GiftPickerDb.UserVotings.Insert(userVoting);

                    this.TempData["ShowAlert"] = Global.ItemCreated;

                    return this.RedirectToAction(nameof(Search));
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, Global.GeneralError);
            }

            model.LoadBase(this.LoggedUser.Id);

            return this.View(model);
        }

        public IActionResult StopVoting(uint id)
        {
            try
            {
                var userVoting = GiftPickerDb.UserVotings.GetById(id);

                if (userVoting == null || !userVoting.IsActive) { return this.NotFound(); }
                if (this.LoggedUser.Id != userVoting.CreatorId) { return this.Forbid(); }

                userVoting.IsActive = false;

                GiftPickerDb.UserVotings.UpdateIsActive(userVoting);

                this.TempData["ShowAlert"] = Global.UserVotingStopped;
            }
            catch (Exception ex)
            {
                this.TempData["ShowAlert"] = Global.GeneralError;
                this.TempData["AlertType"] = AlertTypes.Danger;
            }

            return this.RedirectToAction(nameof(Search));
        }
    }
}