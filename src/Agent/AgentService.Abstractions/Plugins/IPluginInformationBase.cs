namespace AgentService.Abstractions.Plugins;

public interface IPluginInformationBase
{
    string Key { get; }
    string Name { get; }
    string Description { get; }
}