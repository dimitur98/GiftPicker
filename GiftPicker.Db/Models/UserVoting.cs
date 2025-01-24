using Base.Db.Models;
using DapperMySqlMapper.Attributes;

namespace GiftPicker.Db.Models
{
    public class UserVoting : BaseModel<uint>
    {
        [Column(Name = "user_id")]
        public uint UserId { get; set; }
        public User User { get; set; }

        [Column(Name = "creator_id")]
        public uint CreatorId { get; set; }
        public User Creator { get; set; }

        [Column(Name = "year")]
        public uint Year { get; set; }

        [Column(Name = "is_active")]
        public bool IsActive { get; set; }
    }
}
