using MultiAgentOrchestration.Models;

namespace MultiAgentOrchestration.Orchestration;

public interface IMultiAgentOrchestrator
{
    Task<OrchestrationResponse> RunAsync(string request, CancellationToken cancellationToken);
}
