using Microsoft.SemanticKernel;

namespace MultiAgentOrchestration.Agents;

public sealed class PlannerAgent
{
    private readonly Kernel _kernel;

    public PlannerAgent(Kernel kernel)
    {
        _kernel = kernel;
    }

    public async Task<string> CreatePlanAsync(string input, CancellationToken cancellationToken)
    {
        var prompt = $"""
You are a planner agent.
Break the request into concise numbered implementation steps.
Return one step per line.

Request:
{input}
""";

        var result = await _kernel.InvokePromptAsync(prompt, cancellationToken: cancellationToken);
        return result.ToString();
    }
}
