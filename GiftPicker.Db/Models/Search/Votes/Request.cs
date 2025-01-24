using Base.Db.Models.Search;

namespace GiftPicker.Db.Models.Search.Votes
{
    public class Request : BaseRequest
    {
        public uint? UserVotingId { get; set; }
    }
}