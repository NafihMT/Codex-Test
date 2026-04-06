using MultiAgentOrchestration.Agents;
using MultiAgentOrchestration.Models;

namespace MultiAgentOrchestration.Orchestration;

public sealed class MultiAgentOrchestrator : IMultiAgentOrchestrator
{
    private readonly PlannerAgent _planner;
    private readonly CodeAgent _codeAgent;
    private readonly SqlAgent _sqlAgent;
    private readonly ReviewerAgent _reviewer;

    public MultiAgentOrchestrator(
        PlannerAgent planner,
        CodeAgent codeAgent,
        SqlAgent sqlAgent,
        ReviewerAgent reviewer)
    {
        _planner = planner;
        _codeAgent = codeAgent;
        _sqlAgent = sqlAgent;
        _reviewer = reviewer;
    }

    public async Task<OrchestrationResponse> RunAsync(string request, CancellationToken cancellationToken)
    {
        var plan = await _planner.CreatePlanAsync(request, cancellationToken);

        var steps = plan
            .Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Take(8)
            .ToList();

        var codeOutputs = new List<string>();
        foreach (var step in steps)
        {
            codeOutputs.Add(await _codeAgent.GenerateCodeAsync(step, cancellationToken));
        }

        var sqlOutput = await _sqlAgent.GenerateSqlAndExecuteAsync(request, cancellationToken);
        var review = await _reviewer.ReviewAsync(plan, codeOutputs, sqlOutput, cancellationToken);

        return new OrchestrationResponse
        {
            Plan = plan,
            Steps = steps,
            CodeOutputs = codeOutputs,
            SqlOutput = sqlOutput,
            Review = review
        };
    }
}
