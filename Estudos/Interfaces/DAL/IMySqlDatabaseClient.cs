using MySql.Data.MySqlClient;

namespace Estudos.Interfaces.DAL
{
    public interface IMySqlDatabaseClient
    {
        MySqlConnection CreateDatabaseConnection();
    }
}
