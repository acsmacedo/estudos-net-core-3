using Estudos.AppSettings;
using Estudos.Interfaces.DAL;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Estudos.DAL
{
    public class MySqlDatabaseClient : IMySqlDatabaseClient
    {
        private readonly DatabaseAppSettings _settings;

        public MySqlDatabaseClient(IOptions<DatabaseAppSettings> settings)
        {
            _settings = settings.Value;
        }

        public MySqlConnection CreateDatabaseConnection()
        {
            var conn = new MySqlConnection(_settings.ConnectionString);
            conn.Open();

            return conn;
        }
    }
}
