using Microsoft.SemanticKernel;

namespace MultiAgentOrchestration.Agents;

public sealed class SqlAgent
{
    private readonly Kernel _kernel;

    public SqlAgent(Kernel kernel)
    {
        _kernel = kernel;
    }

    public async Task<string> GenerateSqlAndExecuteAsync(string request, CancellationToken cancellationToken)
    {
        var prompt = $"""
You are a SQL agent that can call tool: sql.ExecuteQuery.
Generate one safe SQLite query for this request and call the tool.
Return both the query and execution output.

Request:
{request}
""";

        var result = await _kernel.InvokePromptAsync(prompt, cancellationToken: cancellationToken);
        return result.ToString();
    }
}
