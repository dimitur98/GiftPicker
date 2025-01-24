using Base.Db;
using Base.Db.DbSets;
using GiftPicker.Db.Models;

namespace GiftPicker.Db.Dal
{
    public class Gifts : BaseDbSet<Gift, uint>
    {
        public Gifts(BaseDb db) : base(db, "gift", "g", "id")
        {
        }
    }
}
