using Base.Db.Models;
using DapperMySqlMapper.Attributes;

namespace GiftPicker.Db.Models
{
    public class Vote : BaseModel<uint>
    {
        [Column(Name = "user_voting_id")]
        public uint UserVotingId { get; set; }

        [Column(Name = "gift_id")]
        public uint GiftId { get; set; }
        public Gift Gift { get; set; }

        [Column(Name = "user_id")]
        public uint UserId { get; set; }
        public User User { get; set; }

        public void LoadGift()
        {
            this.Gift = GiftPickerDb.Gifts.GetById(this.GiftId);
        }
    }
}
