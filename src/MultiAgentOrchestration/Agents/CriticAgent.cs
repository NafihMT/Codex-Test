using MultiAgentOrchestration.Models;

namespace MultiAgentOrchestration.Agents;

public sealed class CriticAgent : IAgent
{
    public string Name => "Critic";

    public Task<AgentResult> ExecuteAsync(AgentContext context, CancellationToken cancellationToken)
    {
        if (!context.DraftResponse.Contains("KPI", StringComparison.OrdinalIgnoreCase))
        {
            context.DraftResponse += "\nQuality note: add concrete KPIs before execution.";
        }

        context.DraftResponse += "\nQuality note: sequence work into a 30/60/90 day rollout.";
        return Task.FromResult(new AgentResult(Name, "Reviewed draft and added quality improvements."));
    }
}
