using Microsoft.Data.SqlClient;

namespace WarehouseManager.Infrastructure.Persistence;

internal sealed class SqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqlConnection Create()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
