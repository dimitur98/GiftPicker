using Base.Db.Models.Search;

namespace GiftPicker.Db.Models.Search.UserVotings
{
    public class Request : BaseRequest
    {
        public uint? ExcludedUserId { get; set; }
    }
}
