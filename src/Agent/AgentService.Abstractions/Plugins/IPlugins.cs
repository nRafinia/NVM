namespace AgentService.Abstractions.Plugins;

public interface IPlugins
{
    IReadOnlyList<IPluginInformationBase> GetPlugins { get; }
}