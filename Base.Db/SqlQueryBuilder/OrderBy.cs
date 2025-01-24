namespace Base.Db.SqlQueryBuilder
{
    public class OrderBy
    {
        public string Column { get; set; }
        public bool Descending { get; set; }

        public OrderBy(string column, bool descending = false)
        {
            this.Column = column.Trim();
            this.Descending = descending;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Column)) { return string.Empty; }

            return string.Format("{0} {1}", this.Column, this.Descending ? "DESC" : string.Empty);
        }
    }
}
