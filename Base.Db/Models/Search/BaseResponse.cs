namespace Base.Db.Models.Search
{
    public class BaseResponse<T>
    {
        public List<T> Records { get; set; }

        public long TotalRecords { get; set; }
    }
}