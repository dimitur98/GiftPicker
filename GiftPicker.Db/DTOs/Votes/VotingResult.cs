using DapperMySqlMapper.Attributes;

namespace GiftPicker.Db.DTOs.Votes
{
    public class VotingResult
    {
        [Column(Name = "full_name")]
        public string FullName { get; set; }

        [Column(Name = "gift_name")]
        public string Gift { get; set; }
    }
}
