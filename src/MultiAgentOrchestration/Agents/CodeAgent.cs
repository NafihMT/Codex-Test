using Microsoft.SemanticKernel;

namespace MultiAgentOrchestration.Agents;

public sealed class CodeAgent
{
    private readonly Kernel _kernel;

    public CodeAgent(Kernel kernel)
    {
        _kernel = kernel;
    }

    public async Task<string> GenerateCodeAsync(string step, CancellationToken cancellationToken)
    {
        var prompt = $"""
You are a C# code agent.
Write concise production-style .NET code for this step.
If the step is not code-related, provide a short implementation note.

Step:
{step}
""";

        var result = await _kernel.InvokePromptAsync(prompt, cancellationToken: cancellationToken);
        return result.ToString();
    }
}
