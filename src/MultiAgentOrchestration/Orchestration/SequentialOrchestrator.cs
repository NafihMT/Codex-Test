using MultiAgentOrchestration.Agents;
using MultiAgentOrchestration.Models;

namespace MultiAgentOrchestration.Orchestration;

public sealed class SequentialOrchestrator
{
    private readonly IReadOnlyList<IAgent> _agents;

    public SequentialOrchestrator(IReadOnlyList<IAgent> agents)
    {
        _agents = agents;
    }

    public async Task<OrchestrationResult> RunAsync(string userPrompt, CancellationToken cancellationToken)
    {
        var context = new AgentContext
        {
            UserPrompt = userPrompt
        };

        var trace = new List<AgentResult>();

        foreach (var agent in _agents)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var stepResult = await agent.ExecuteAsync(context, cancellationToken);
            trace.Add(stepResult);
        }

        return new OrchestrationResult
        {
            FinalResponse = context.DraftResponse,
            Trace = trace
        };
    }
}
