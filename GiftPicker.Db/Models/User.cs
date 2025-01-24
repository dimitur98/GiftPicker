using Base.Db.Models;
using DapperMySqlMapper.Attributes;

namespace GiftPicker.Db.Models
{
    public class User : BaseModel<uint>
    {
        [Column(Name = "username")]
        public string Username { get; set; }

        [Column(Name = "password")]
        public string Password { get; set; }

        [Column(Name = "full_name")]
        public string FullName { get; set; }

        [Column(Name = "birthday")]
        public DateTime Birthday { get; set; }
    }
}
