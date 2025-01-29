using Base.Db.Models;
using Base.Db.SqlQueryBuilder;
using System.Data;

namespace Base.Db.DbSets
{
    public abstract class BaseDbSet
    {
        protected readonly BaseDb _db;
        protected BaseDbSet(BaseDb db)
        {
            _db = db;
        }

        protected long QueryCount(Query query, object param = null)
        {
            var totalsQuery = new Query(new string[] { "COUNT(*)" }, query.From, joins: query.Joins, where: query.Where, groupBy: query.GroupBy);
            var count = _db.Mapper.Query<long?>(totalsQuery.Build(), param: param).FirstOrDefault();

            return count == null ? 0 : count.Value;
        }
    }

    public abstract class BaseDbSet<T, TId> : BaseDbSet
        where T : BaseModel<TId>
    {
        protected readonly BaseDb _db;
        protected readonly string _table;
        protected readonly string _tableAlias;
        protected readonly string _id;

        protected BaseDbSet(BaseDb db, string table, string tableAlias, string id)
            : base(db)
        {
            _db = db;
            _table = table;
            _tableAlias = tableAlias;
            _id = id;
        }

        public virtual List<T> GetAll()
        {
            var sql = $@"SELECT *
                FROM {_table}";

            return _db.Mapper.Query<T>(sql).ToList();
        }

        public virtual T GetById(TId id) => this.GetByIds([id]).FirstOrDefault();

        public virtual List<T> GetByIds(IEnumerable<TId> ids)
        {
            var sql = $@"SELECT *
                FROM {_table} {_tableAlias}
                WHERE {_tableAlias}.{_id} IN @ids";

            return _db.Mapper.Query<T>(sql, new { ids }).ToList();
        }

        protected virtual T GetFirstByField<TValue>(string field, TValue value)
        {
            var sql = @$"SELECT *
                FROM {_table}
                WHERE {field} = @value
                LIMIT 1";

            return _db.Mapper.Query<T>(sql, param: new { value }).FirstOrDefault();
        }

        protected virtual void Insert(T model, string sql, object param = null)
        {
            sql = sql.Trim();
            if (!sql.EndsWith(";")) { sql += ";"; }
            sql += "\n\nSELECT LAST_INSERT_ID() AS id;";

            model.Id = _db.Mapper.Query<TId>(sql, param: param).FirstOrDefault();
        }

        protected virtual void Update(string sql, object param = null)
        {
            _db.Mapper.Execute(sql, param: param);
        }

        protected void LoadEntities<TEntity, TEntityId, TPKeyEntity>(IEnumerable<TEntity> entities,
         Func<TEntity, TEntityId> pKeySelector,
         Func<IEnumerable<TEntityId>, IEnumerable<TPKeyEntity>> pKeyEntitiesSelector,
         Action<TEntity, IEnumerable<TPKeyEntity>> map)
         where TEntity : class
         where TPKeyEntity : class
        {
            if (entities?.Any() == null) { return; }

            var pKeyIds = entities.Select(pKeySelector).Distinct().ToList();

            if (pKeyIds?.Any() == null) { return; }

            var pKeyEntities = pKeyEntitiesSelector(pKeyIds);

            foreach (var entity in entities)
            {
                map(entity, pKeyEntities);
            }
        }
    }
}