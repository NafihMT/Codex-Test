namespace MultiAgentOrchestration.Models;

public sealed class AgentContext
{
    public required string UserPrompt { get; init; }
    public string Plan { get; set; } = string.Empty;
    public string ResearchNotes { get; set; } = string.Empty;
    public string DraftResponse { get; set; } = string.Empty;
}
