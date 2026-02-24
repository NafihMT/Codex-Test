namespace MultiAgentOrchestration.Models;

public sealed class OrchestrationResult
{
    public required string FinalResponse { get; init; }
    public required IReadOnlyList<AgentResult> Trace { get; init; }
}
