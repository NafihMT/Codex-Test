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
Generate one safe SQLite SELECT query for this request, then call the tool.
Return:
1) The SQL query
2) Tool output

Request:
{request}
""";

        var settings = new PromptExecutionSettings
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        };

        var arguments = new KernelArguments(settings)
        {
            ["request"] = request
        };

        var result = await _kernel.InvokePromptAsync(prompt, arguments, cancellationToken: cancellationToken);
        return result.ToString();
    }
}
