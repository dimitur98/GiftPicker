using Base.Db;
using Base.Db.DbSets;
using GiftPicker.Db.Models;
using GiftPicker.Db.Models.Search.Users;

namespace GiftPicker.Db.Dal
{
    public class Users : BaseSearchableDbSet<User, uint, Request, Response>
    {
        private const uint Sha2Bits = 256;

        public Users(BaseDb db) : base(db, "user", "u", "id", "full_name")
        {
        }

        public User GetByUsername(string username) => base.GetFirstByField<string>("username", username);

        public User GetByUsernameAndPassword(string username, string password)
        {
            string sql = @"
                SELECT u.*
                FROM `user` u 
                WHERE u.username = @username
                    AND u.password = sha2(@password, @sha2Bits)";

            return _db.Mapper.Query<User>(sql, new { username, password, Sha2Bits }).SingleOrDefault();
        }

        public List<User> GetAll(uint excludedId)
        {
            var sql = @"
                SELECT u.*
                FROM `user` u
                WHERE u.id <> @excludedId
                ORDER BY MONTH(u.birthday), DAY(u.birthday)";

            return _db.Mapper.Query<User>(sql, param: new { excludedId }).ToList();
        }
    }
}
