using Base.Db.Models;
using DapperMySqlMapper.Attributes;

namespace GiftPicker.Db.Models
{
    public class Gift : BaseModel<uint>
    {
        [Column(Name = "name")]
        public string Name { get; set; }
    }
}
