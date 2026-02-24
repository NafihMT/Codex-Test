using System.ComponentModel;
using Microsoft.Data.Sqlite;
using Microsoft.SemanticKernel;

namespace MultiAgentOrchestration.Tools;

public sealed class SqlTool
{
    private readonly string _connectionString;

    public SqlTool(string connectionString)
    {
        _connectionString = connectionString;
        EnsureSchema();
    }

    [KernelFunction("ExecuteQuery")]
    [Description("Executes a read-only SQLite query and returns rows as text.")]
    public string ExecuteQuery(string query)
    {
        if (!query.TrimStart().StartsWith("select", StringComparison.OrdinalIgnoreCase))
        {
            return "Only SELECT queries are allowed in this demo tool.";
        }

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = query;

        using var reader = command.ExecuteReader();

        var rows = new List<string>();
        while (reader.Read())
        {
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                cols.Add($"{reader.GetName(i)}={reader.GetValue(i)}");
            }

            rows.Add(string.Join(", ", cols));
        }

        return rows.Count == 0 ? "No rows returned." : string.Join("\n", rows);
    }

    private void EnsureSchema()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var create = connection.CreateCommand();
        create.CommandText =
            """
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY,
                Name TEXT NOT NULL,
                Role TEXT NOT NULL
            );

            INSERT INTO Users (Name, Role)
            SELECT 'Alice', 'Admin'
            WHERE NOT EXISTS (SELECT 1 FROM Users WHERE Name = 'Alice');

            INSERT INTO Users (Name, Role)
            SELECT 'Bob', 'Developer'
            WHERE NOT EXISTS (SELECT 1 FROM Users WHERE Name = 'Bob');
            """;

        create.ExecuteNonQuery();
    }
}
