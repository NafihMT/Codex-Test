namespace MultiAgentOrchestration.Models;

public sealed class OrchestrationResponse
{
    public required string Plan { get; init; }
    public required IReadOnlyList<string> Steps { get; init; }
    public required IReadOnlyList<string> CodeOutputs { get; init; }
    public required string SqlOutput { get; init; }
    public required string Review { get; init; }
}
