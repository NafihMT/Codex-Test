using MultiAgentOrchestration.Models;

namespace MultiAgentOrchestration.Agents;

public sealed class ResearchAgent : IAgent
{
    public string Name => "Research";

    public Task<AgentResult> ExecuteAsync(AgentContext context, CancellationToken cancellationToken)
    {
        context.ResearchNotes =
            $"Prompt: {context.UserPrompt}\n" +
            "- Competitive angle: free tools must win on usability and speed\n" +
            "- Distribution: student communities, creator partnerships, and campus ambassadors\n" +
            "- KPI baseline: activation %, week-1 retention, and referral rate";

        return Task.FromResult(new AgentResult(Name, "Prepared contextual research notes."));
    }
}
