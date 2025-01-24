using DapperMySqlMapper;

namespace Base.Db
{
    public abstract class BaseDb
    {
        public MySqlMapper Mapper { get; set; }

        protected BaseDb(string connectionString, int? unableToConnectToHostErrorRetryInterval = null, bool unableToConnectToHostErrorRetryTillConnect = false)
        {
            this.Mapper = new MySqlMapper(connectionString, unableToConnectToHostErrorRetryInterval: unableToConnectToHostErrorRetryInterval, unableToConnectToHostErrorRetryTillConnect: unableToConnectToHostErrorRetryTillConnect);
        }
    }
}
