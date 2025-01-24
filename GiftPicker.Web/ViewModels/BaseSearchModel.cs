using Base.Db.Models.Search;

namespace GiftPicker.Web.ViewModels
{
    public class BaseSearchModel <TResponse, TModel>
        where TResponse : BaseResponse<TModel>
    {
        public TResponse Response { get; set; }
    }
}