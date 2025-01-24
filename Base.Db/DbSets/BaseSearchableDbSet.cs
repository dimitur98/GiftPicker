using Base.Db.Models;
using Base.Db.Models.Search;
using Base.Db.SqlQueryBuilder;

namespace Base.Db.DbSets
{
    public abstract class BaseSearchableDbSet<T, TId, TSearchRequest, TSearchResponse> : BaseDbSet<T, TId>
        where T : BaseModel<TId>
        where TSearchRequest : IBaseRequest
        where TSearchResponse : BaseResponse<T>, new()
    {
        protected readonly string _sortField;
        protected readonly bool _sortDesc;

        protected BaseSearchableDbSet(BaseDb db, string table, string tableAlias, string id, string sortField, bool sortDesc = false)
        : base(db, table, tableAlias, id)
        { 
            _sortField = sortField;
            _sortDesc = sortDesc;
        }

        protected virtual Query GetSearchQuery(TSearchRequest request)
        {
            if (string.IsNullOrEmpty(request.SortColumn))
            {
                request.SortColumn = _sortField;
                request.SortDesc = _sortDesc;
            }

            return new Query
            {
                Select = new List<string>{ "*" },
                From = $"{_table} {_tableAlias}",
                Joins = new List<string>(),
                Where = new List<string>() { "1 = 1" },
                OrderBys = new List<OrderBy>() { new OrderBy(request.SortColumn, request.SortDesc) }
            };
        }

        protected virtual object GetSearchQueryParams(TSearchRequest request)
        {
            return null;
        }

        public virtual TSearchResponse Search(TSearchRequest request)
        {
            var query = this.GetSearchQuery(request);
            var response = new TSearchResponse();

            using (var connection = _db.Mapper.GetConnection())
            {
                var queryParams = this.GetSearchQueryParams(request);

                if (request.ReturnTotalRecords)
                {
                    response.TotalRecords = this.QueryCount(query, param: queryParams);

                    if (response.TotalRecords <= 0) { return response; }
                }

                if (request.ReturnRecords)
                {
                    response.Records = _db.Mapper.Query<T>(query.Build(), param: queryParams).ToList();
                }
            }

            return response;
        }
    }
}
