using GiftPicker.Db;
using GiftPicker.Db.Dal;
using GiftPicker.Db.Models;
using GiftPicker.Web.Resources;
using GiftPicker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GiftPicker.Web.Controllers
{
    public class VotesController : BaseController
    {
        public IActionResult VotingResult(uint userVotingId)
        {
            var userVoting = GiftPickerDb.UserVotings.GetById(userVotingId);

            if (userVoting == null) { return this.NotFound(); }
            if (userVoting.IsActive) { return this.Forbid(); }

            var votingResult = GiftPickerDb.Votes.GetVotingResult(userVotingId);

            return this.View(votingResult);
        }

        public IActionResult Create(uint userVotingId)
        {
            var userVoting = GiftPickerDb.UserVotings.GetById(userVotingId);

            if (userVoting == null) { return this.NotFound(); }
            if (this.LoggedUser.Id == userVoting.UserId) { return this.Forbid(); }

            var model = new VoteCreateModel
            {
                UserVotingId = userVotingId
            };

            model.LoadBase();

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Create(VoteCreateModel model)
        {
            try
            {
                if (this.ModelState.IsValid && GiftPickerDb.Votes.Exists(model.UserVotingId.Value, this.LoggedUser.Id))
                {
                    this.ModelState.AddModelError(string.Empty, Global.VoteExists);
                }

                if (this.ModelState.IsValid)
                {
                    var userVoting = GiftPickerDb.UserVotings.GetById(model.UserVotingId.Value);

                    if (userVoting == null) { return this.NotFound(); }

                    if (this.LoggedUser.Id == userVoting.UserId) { return this.Forbid(); }

                    var vote = new Vote
                    {
                        UserVotingId = model.UserVotingId.Value,
                        GiftId = model.GiftId.Value,
                        UserId = this.LoggedUser.Id
                    };

                    GiftPickerDb.Votes.Insert(vote);

                    this.TempData["ShowAlert"] = Global.ItemCreated;

                    return this.RedirectToAction("Search", "UserVotings");
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, Global.GeneralError);
            }

            model.LoadBase();

            return this.View(model);
        }

        public IActionResult Details(uint userVotingId)
        {
            var userVoting = GiftPickerDb.UserVotings.GetById(userVotingId);

            if (userVoting == null) { return this.NotFound(); }

            var vote = GiftPickerDb.Votes.Get(userVotingId, this.LoggedUser.Id);

            if (vote == null) { return this.NotFound(); }

            vote.LoadGift();

            var model = new VoteDetailsModel
            {
                GiftName = vote.Gift.Name,
            };

            return this.View(model);
        }
    }
}
