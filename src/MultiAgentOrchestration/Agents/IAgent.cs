using MultiAgentOrchestration.Models;

namespace MultiAgentOrchestration.Agents;

public interface IAgent
{
    string Name { get; }
    Task<AgentResult> ExecuteAsync(AgentContext context, CancellationToken cancellationToken);
}
