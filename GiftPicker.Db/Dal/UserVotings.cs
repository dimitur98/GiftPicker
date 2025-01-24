using Base.Db;
using Base.Db.DbSets;
using Base.Db.SqlQueryBuilder;
using GiftPicker.Db.Models;
using GiftPicker.Db.Models.Search.UserVotings;

namespace GiftPicker.Db.Dal
{
    public class UserVotings : BaseSearchableDbSet<UserVoting, uint, Request, Response>
    {
        public UserVotings(BaseDb db) : base(db, "user_voting", "uv", "id", "id")
        {
        }

        protected override Query GetSearchQuery(Request request)
        {
            var query = base.GetSearchQuery(request);

            if (request.ExcludedUserId.HasValue) { query.Where.Add("AND uv.user_id <> @excludedUserId"); }
            if (!string.IsNullOrEmpty(request.SortColumn) && request.SortColumn.Contains("u.")) 
            {
                query.Select = new List<string> { "uv.*"};
                query.Joins.Add("JOIN user u ON u.id = uv.user_id"); 
            }

            return query;
        }

        protected override object GetSearchQueryParams(Request request)
        {
            return new
            {
               request.ExcludedUserId
            };
        }

        public List<uint> GetVotedIds(uint userId)
        {
            var sql = @"SELECT uv.id
                FROM user_voting uv
                WHERE uv.is_active = TRUE
                    AND EXISTS(SELECT * FROM vote v WHERE v.user_voting_id = uv.id AND v.user_id = @userId)";

            return _db.Mapper.Query<uint>(sql, new { userId }).ToList();
        }

        public bool Exists(uint userId, uint year)
        {
            var sql = @"SELECT 1
                FROM user_voting uv
                WHERE uv.user_id = @userId
                    AND uv.year = @year
                LIMIT 1";

            return _db.Mapper.Query<int>(sql, new { userId, year }).FirstOrDefault() == 1;
        }

        public void Insert(UserVoting userVoting)
        {
            base.Insert(userVoting,
                @"INSERT INTO `user_voting` (
                    `user_id`, 
                    `year`,
	                `creator_id`
	            )
	            VALUES
	            (
		            @userId,
                    @year,
		            @creatorId
	            );",
                param: new
                {
                    userId = userVoting.UserId,
                    year = userVoting.Year,
                    creatorId = userVoting.CreatorId
                });
        }

        public void UpdateIsActive(UserVoting userVoting)
        {
            base.Update(
                @"UPDATE user_voting SET
                    `is_active` = @isActive
                WHERE id = @id",
                param: new
                {
                    id = userVoting.Id,
                    isActive = userVoting.IsActive
                });
        }

        public void LoadUsers(IEnumerable<UserVoting> userVotings)
        {
            base.LoadEntities(userVotings, uv => uv.UserId, userIds => GiftPickerDb.Users.GetByIds(userIds), (u, users) => u.User = users.FirstOrDefault(x => x.Id == u.UserId));
        }

        public void LoadCreators(IEnumerable<UserVoting> userVotings)
        {
            base.LoadEntities(userVotings, uv => uv.CreatorId, creatorIds => GiftPickerDb.Users.GetByIds(creatorIds), (u, users) => u.Creator = users.FirstOrDefault(x => x.Id == u.CreatorId));
        }
    }
}
