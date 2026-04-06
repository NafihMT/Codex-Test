using Microsoft.SemanticKernel;

namespace MultiAgentOrchestration.Agents;

public sealed class ReviewerAgent
{
    private readonly Kernel _kernel;

    public ReviewerAgent(Kernel kernel)
    {
        _kernel = kernel;
    }

    public async Task<string> ReviewAsync(string plan, IReadOnlyList<string> codeOutputs, string sqlOutput, CancellationToken cancellationToken)
    {
        var prompt = $"""
You are a reviewer agent.
Review the plan, generated code snippets, and SQL output.
Provide:
1) Issues
2) Improvements
3) Final polished response

Plan:
{plan}

Code outputs:
{string.Join("\n\n---\n\n", codeOutputs)}

SQL output:
{sqlOutput}
""";

        var result = await _kernel.InvokePromptAsync(prompt, cancellationToken: cancellationToken);
        return result.ToString();
    }
}
