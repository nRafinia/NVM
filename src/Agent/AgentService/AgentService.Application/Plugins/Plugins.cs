using AgentService.Abstractions.Plugins;

namespace AgentService.Application.Plugins;

public class Plugins : IPlugins
{
    public IReadOnlyList<IPluginInformationBase> GetPlugins => PluginCollection.GetPlugins;
}