using GiftPicker.Db;
using GiftPicker.Db.Models;
using GiftPicker.Db.Models.Search.Votes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GiftPicker.Web.ViewModels
{
    public class VotesSearchModel : BaseSearchModel<Response, Vote>
    {
    }

    public class VoteCreateModel 
    {
        [Required]
        public uint? UserVotingId { get; set; }

        [Required]
        [DisplayName("Gift")]
        public uint? GiftId { get; set; }
        public List<Gift> Gifts { get; set; }

        public void LoadBase()
        {
            this.Gifts = GiftPickerDb.Gifts.GetAll();
        }
    }

    public class VoteDetailsModel {
        [DisplayName("Gift")]
        public string GiftName { get; set; }
    }
}