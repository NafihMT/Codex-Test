using Microsoft.SemanticKernel;
using MultiAgentOrchestration.Agents;
using MultiAgentOrchestration.Orchestration;
using MultiAgentOrchestration.Tools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(sp =>
{
    var kernelBuilder = Kernel.CreateBuilder();

    kernelBuilder.AddOllamaChatCompletion(
        modelId: "llama3",
        endpoint: new Uri("http://localhost:11434"));

    var kernel = kernelBuilder.Build();
    kernel.Plugins.AddFromObject(new SqlTool("Data Source=multiagent.db"), "sql");

    return kernel;
});

builder.Services.AddScoped<PlannerAgent>();
builder.Services.AddScoped<CodeAgent>();
builder.Services.AddScoped<SqlAgent>();
builder.Services.AddScoped<ReviewerAgent>();
builder.Services.AddScoped<IMultiAgentOrchestrator, MultiAgentOrchestrator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
