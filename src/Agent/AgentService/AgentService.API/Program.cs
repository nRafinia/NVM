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

var pluginAssemblies = PluginFinder.GetList();
assemblies.AddRange(pluginAssemblies);

PluginCollection.Initialize(PluginFinder.GetList());

builder.Services
    .AddDomainServices()
    .AddInfraServices(builder.Configuration)
    .AddApplicationServices(assemblies.ToArray())
    .AddPresentationServices(builder.Configuration);

foreach (var plugin in PluginCollection.GetPlugins)
{
    plugin.AddPluginService(builder.Services, builder.Configuration);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
app.AddPipelines();

foreach (var plugin in PluginCollection.GetPlugins)
{
    var endpointRoute = $"/{plugin.Name.Replace(" ","")}";
    var endpointTag = plugin.Name;
    var group = app.MapGroup(endpointRoute).WithTags(endpointTag);
    plugin.AddEndpoints(group, endpointTag);
}

app.Run();