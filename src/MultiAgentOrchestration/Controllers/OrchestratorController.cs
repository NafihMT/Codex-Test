using Microsoft.AspNetCore.Mvc;
using MultiAgentOrchestration.Models;
using MultiAgentOrchestration.Orchestration;

namespace MultiAgentOrchestration.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OrchestratorController : ControllerBase
{
    private readonly IMultiAgentOrchestrator _orchestrator;

    public OrchestratorController(IMultiAgentOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    [HttpPost]
    public async Task<IActionResult> Run([FromBody] OrchestrationRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Request))
        {
            return BadRequest("Request is required.");
        }

        var result = await _orchestrator.RunAsync(request.Request, cancellationToken);
        return Ok(result);
    }
}
