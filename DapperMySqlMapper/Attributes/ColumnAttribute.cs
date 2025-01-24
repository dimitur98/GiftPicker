namespace DapperMySqlMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute() { }

        public string Name { get; set; }

        public string Storage { get; set; }
    }
}
