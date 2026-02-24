# Free Multi-Agent Orchestration in .NET (ASP.NET Core + Semantic Kernel + Ollama + SQLite)

This project implements the architecture from your chat as a practical, resume-ready baseline:

- ASP.NET Core Web API
- Microsoft Semantic Kernel orchestration
- Ollama local model (`llama3`)
- SQLite-backed MCP-style tool calling
- Four agents: Planner, Code, SQL, Reviewer

## 1) Prerequisites (all free)

1. Install Ollama: https://ollama.com
2. Pull and run a local model:
   ```bash
   ollama pull llama3
   ollama run llama3
   ```
3. Verify Ollama endpoint:
   - `http://localhost:11434`
4. Install .NET 8 SDK

## 2) Run the API

```bash
dotnet restore src/MultiAgentOrchestration/MultiAgentOrchestration.csproj
dotnet run --project src/MultiAgentOrchestration
```

Swagger (development):
- `http://localhost:5000/swagger` or `http://localhost:5xxx/swagger`

## 3) Call the orchestrator

Endpoint:
- `POST /api/orchestrator`

Sample request:

```bash
curl -X POST http://localhost:5000/api/orchestrator \
  -H "Content-Type: application/json" \
  -d '{"request":"Create a CRUD API for Product"}'
```

## 4) Agent flow

1. **PlannerAgent** creates a numbered plan.
2. **CodeAgent** generates code per step.
3. **SqlAgent** asks the model to call `sql.ExecuteQuery`.
4. **ReviewerAgent** critiques and polishes the final output.

## 5) MCP-style tool calling

`SqlTool` is registered as a Semantic Kernel plugin (`sql`) with a callable function `ExecuteQuery`.

- Database file: `multiagent.db`
- Seeded table: `Users`
- Safety guard: demo only allows `SELECT` statements

This gives a local, free, tool-augmented orchestration flow similar to MCP-like patterns.
