using Dapper;
using DapperMySqlMapper.Attributes;
using MySql.Data.MySqlClient;
using System.Reflection;

namespace DapperMySqlMapper
{
    public class MySqlMapper
    {
        public string ConnectionString { get; set; }
        public int? UnableToConnectToHostErrorRetryInterval { get; private set; }
        public bool UnableToConnectToHostErrorRetryTillConnect { get; private set; }

        public MySqlMapper(string connectionString, int? unableToConnectToHostErrorRetryInterval = null, bool unableToConnectToHostErrorRetryTillConnect = false)
        {
            this.ConnectionString = connectionString;
            this.UnableToConnectToHostErrorRetryInterval = unableToConnectToHostErrorRetryInterval;
            this.UnableToConnectToHostErrorRetryTillConnect = unableToConnectToHostErrorRetryTillConnect;
        }

        public MySqlConnection GetConnection(bool openConnection = true, bool retryOnFailure = true)
        {
            var conn = new MySqlConnection(this.ConnectionString);

            if (openConnection)
            {
                try
                {
                    conn.Open();
                }
                catch (MySqlException ex)
                {
                    int? retryInterval = this.UnableToConnectToHostErrorRetryInterval;
                    bool retryTillConnect = this.UnableToConnectToHostErrorRetryTillConnect;

                    if (retryTillConnect == true && retryInterval.HasValue == false) { retryInterval = 5000; }

                    if (ex.Number == (int)MySqlErrorCode.UnableToConnectToHost
                        && retryOnFailure == true
                        && retryInterval.HasValue
                        && retryInterval.Value > 0)
                    {
                        Task.Delay(retryInterval.Value).Wait();

                        return this.GetConnection(openConnection, retryTillConnect);
                    }

                    throw;
                }
                catch
                {
                    throw;
                }
            }

            return conn;
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            SqlMapper.SetTypeMap(typeof(T), new CustomPropertyTypeMap(typeof(T), (type, columnName) =>
                                 type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                     .FirstOrDefault(prop => prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName))
                                 )
                        );

            using (var conn = this.GetConnection())
            {
                return SqlMapper.Query<T>(conn, sql, param);
            }
        }

        public int Execute(string sql, object param = null)
        {
            using (var conn = this.GetConnection())
            {
                return SqlMapper.Execute(conn, sql, param);
            }
        }
    }
}