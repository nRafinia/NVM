using System.Reflection;
using AgentService.API;
using AgentService.Application;
using AgentService.Application.Plugins;
using AgentService.Domain;
using AgentService.Infra;
using AgentService.Infra.Plugins;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assemblies = new List<Assembly>()
{
    typeof(Shared.Domain.ConfigureServices).Assembly,
    typeof(AgentService.Domain.ConfigureServices).Assembly,
    typeof(Shared.Infra.ConfigureServices).Assembly,
    typeof(AgentService.Infra.ConfigureServices).Assembly,
    typeof(Shared.Application.ConfigureServices).Assembly,
    typeof(AgentService.Application.ConfigureServices).Assembly,
    typeof(Shared.Presentation.ConfigureServices).Assembly,
    typeof(Program).Assembly,
};

PluginCollection.Initialize(PluginFinder.GetList());

builder.Services
    .AddDomainServices()
    .AddInfraServices(builder.Configuration)
    .AddApplicationServices(assemblies.ToArray())
    .AddPresentationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.AddPipelines();

app.Run();