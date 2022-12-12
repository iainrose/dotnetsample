using System.Text;
using Microsoft.Data.SqlClient;

namespace SnykTest;

class DataExampleWithStringBuilder : DataExampleBase
{
    public override async Task Process(string name)
    {
        await using var connection = new SqlConnection();
        await connection.OpenAsync();
        var command = new StringBuilder();
        command.Append("SELECT password FROM dbo.Users ");
        command.Append("WHERE [Name]='");
        command.Append(name);
        command.Append('\'');
        await using var cmd = new SqlCommand(command.ToString(), connection);
        var payload = await cmd.ExecuteScalarAsync();
        if (payload.ToString() != name + name)
            throw new UnauthorizedAccessException();
    }
}