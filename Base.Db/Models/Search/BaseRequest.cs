namespace Base.Db.Models.Search
{
    public interface IBaseRequest
    {
        string SortColumn { get; set; }
        bool SortDesc { get; set; }
        bool ReturnTotalRecords { get; set; }
        bool ReturnRecords { get; set; }
    }

    public abstract class BaseRequest : IBaseRequest
    {
        public string SortColumn { get; set; }
        public bool SortDesc { get; set; }

        public bool ReturnTotalRecords { get; set; } = true;
        public bool ReturnRecords { get; set; } = true;
    }
}