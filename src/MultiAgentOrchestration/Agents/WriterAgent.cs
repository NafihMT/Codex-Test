using MultiAgentOrchestration.Models;

namespace MultiAgentOrchestration.Agents;

public sealed class WriterAgent : IAgent
{
    public string Name => "Writer";

    public Task<AgentResult> ExecuteAsync(AgentContext context, CancellationToken cancellationToken)
    {
        context.DraftResponse =
$"""
Recommended response structure
-----------------------------
User goal
{context.UserPrompt}

Execution plan
{context.Plan}

Supporting insights
{context.ResearchNotes}

Suggested next step
Run a one-week pilot with a single target audience and compare activation/retention against baseline.
""";

        return Task.FromResult(new AgentResult(Name, "Produced a consolidated draft response."));
    }
}
