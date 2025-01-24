using GiftPicker.Db;
using GiftPicker.Db.Models;
using GiftPicker.Db.Models.Search.UserVotings;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GiftPicker.Web.ViewModels
{
    public class UserVotingSearchModel : BaseSearchModel<Response, UserVoting>
    {
        public IEnumerable<uint> VotedUserVotingIds { get; set; }
    }

    public class  UserVotingBaseModel
    {
        [Required]
        [DisplayName("User")]
        public uint? UserId { get; set; }
        public IEnumerable<User> Users { get; set; }

        [Required]
        public uint? Year { get; set; }
        public IEnumerable<int> Years { get; set; }

        public void LoadBase(uint loggedUserId)
        {
            this.Users = GiftPickerDb.Users.GetAll(loggedUserId);
            this.Years = Enumerable.Range(DateTime.Now.Year, 5);
        }
    }

    public class UserVotingCreateModel : UserVotingBaseModel
    {
    }
}