using System.Reflection;
using AgentService.Abstractions.Plugins;
using Shared.Domain.Shared;

namespace AgentService.Application.Plugins;

public static class PluginCollection
{
    private static readonly IList<IPluginInformation> Plugins = new List<IPluginInformation>();
    
    public static IReadOnlyList<IPluginInformation> GetPlugins => Plugins.AsReadOnly();
    
    public static void Initialize(params Assembly[] assemblies)
    {
        var plugins = Common.GetImplementedInterfaceOf(typeof(IPluginInformation), assemblies);

        foreach (var plugin in plugins)
        {
            Plugins.Add((IPluginInformation)Activator.CreateInstance(plugin)!); 
        }
    }
}