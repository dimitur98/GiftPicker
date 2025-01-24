using DapperMySqlMapper.Attributes;

namespace Base.Db.Models
{
    public class BaseModel<T>
    {
        [Column(Name = "id")]
        public T Id { get; set; }
    }
}
