using Microsoft.Data.SqlClient;

namespace SnykTest;

class DataExampleWithString : DataExampleBase
{
    public override async Task Process(string name)
    {
        await using var connection = new SqlConnection();
        await connection.OpenAsync();
        var command = "SELECT password FROM dbo.Users WHERE [Name]='" + name + "'";
        await using var cmd = new SqlCommand(command.ToString(), connection);
        var payload = await cmd.ExecuteScalarAsync();
        if (payload.ToString() != name + name)
            throw new UnauthorizedAccessException();
    }
}