using Base.Db;
using Base.Db.DbSets;
using Base.Db.SqlQueryBuilder;
using GiftPicker.Db.DTOs.Votes;
using GiftPicker.Db.Models;
using GiftPicker.Db.Models.Search.Votes;

namespace GiftPicker.Db.Dal
{
    public class Votes : BaseSearchableDbSet<Vote, uint, Request, Response>
    {
        public Votes(BaseDb db) : base(db, "vote", "v", "id", "v.gift_id")
        {
        }

        protected override Query GetSearchQuery(Request request)
        {
            var query = base.GetSearchQuery(request);

            if (request.UserVotingId.HasValue) { query.Where.Add("AND v.user_voting_id = @userVotingId"); }

            return query;
        }

        protected override object GetSearchQueryParams(Request request)
        {
            return new
            {
                request.UserVotingId
            };
        }

        public List<VotingResult> GetVotingResult(uint userVotingId)
        {
            var sql = @"SELECT u.full_name AS full_name, g.name AS gift_name
                FROM `user` u
                LEFT JOIN `vote` v ON v.user_id = u.id AND (v.user_voting_id = @userVotingId OR v.user_voting_id IS NULL)
                LEFT JOIN `gift` g ON g.id = v.gift_id
                ORDER BY v.gift_id DESC";

            return _db.Mapper.Query<VotingResult>(sql, new { userVotingId }).ToList();
        }

        public Vote Get(uint userVotingId, uint userId)
        {
            var sql = @"SELECT *
                FROM vote v
                WHERE v.user_voting_id = @userVotingId
                    AND v.user_id = @userId";

            return _db.Mapper.Query<Vote>(sql, new { userVotingId, userId }).FirstOrDefault();
        }

        public bool Exists(uint userVotingId, uint userId)
        {
            var sql = @"SELECT 1
                FROM vote v
                WHERE v.user_voting_id = @userVotingId
                    AND v.user_id = @userId";

            return _db.Mapper.Query<int>(sql, new { userVotingId, userId }).FirstOrDefault() == 1;
        }

        public void Insert(Vote vote)
        {
            base.Insert(vote,
                @"INSERT INTO `vote` (
                    `user_voting_id`, 
                    `gift_id`,
	                `user_id`
	            )
	            VALUES
	            (
		            @userVotingId,
                    @giftId,
		            @userId
	            );",
                param: new
                {
                    userVotingId = vote.UserVotingId,
                    giftId = vote.GiftId,
                    userId = vote.UserId
                });
        }
    }
}