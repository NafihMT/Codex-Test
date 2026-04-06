# Free Multi-Agent Orchestration in .NET (ASP.NET Core + Semantic Kernel + Ollama + SQLite)

This project implements the architecture from your chat as a practical, resume-ready baseline:

- ASP.NET Core Web API
- Microsoft Semantic Kernel orchestration
- Ollama local model (`llama3`)
- SQLite-backed MCP-style tool calling
- Four agents: Planner, Code, SQL, Reviewer

## How to run locally

### 1) Install prerequisites

1. Install **.NET 8 SDK**
2. Install **Ollama**: https://ollama.com
3. Pull and run model:

```bash
ollama pull llama3
ollama run llama3
```

4. Verify Ollama is reachable:

```bash
curl http://localhost:11434
```

### 2) Restore and run API

From repo root:

```bash
dotnet restore src/MultiAgentOrchestration/MultiAgentOrchestration.csproj
dotnet run --project src/MultiAgentOrchestration
```

### 3) Open Swagger

When app starts, open the printed URL + `/swagger`.
Common URLs:

- `http://localhost:5000/swagger`
- `https://localhost:5001/swagger`

### 4) Test the orchestration endpoint

`POST /api/orchestrator`

```bash
curl -X POST http://localhost:5000/api/orchestrator \
  -H "Content-Type: application/json" \
  -d '{"request":"Create a CRUD API for Product"}'
```

PowerShell equivalent:

```powershell
Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5000/api/orchestrator" `
  -ContentType "application/json" `
  -Body '{"request":"Create a CRUD API for Product"}'
```

## Agent flow

1. **PlannerAgent** creates a numbered plan.
2. **CodeAgent** generates code per step.
3. **SqlAgent** generates a SQLite query and can call `sql.ExecuteQuery`.
4. **ReviewerAgent** critiques and polishes the final output.

## MCP-style tool calling

`SqlTool` is registered as a Semantic Kernel plugin (`sql`) with a callable function `ExecuteQuery`.

- Database file: `multiagent.db`
- Seeded table: `Users`
- Safety guard: demo only allows `SELECT` statements

This gives a local, free, tool-augmented orchestration flow similar to MCP-like patterns.
