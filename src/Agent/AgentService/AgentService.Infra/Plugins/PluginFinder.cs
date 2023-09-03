using System.Reflection;

namespace AgentService.Infra.Plugins;

public static class PluginFinder
{
    public static Assembly[] GetList()
    {
        var assemblies = new List<Assembly>
        {
            typeof(Service.API.PluginInformation).Assembly,
            typeof(FileManager.API.PluginInformation).Assembly,
            typeof(HardwareInfo.API.PluginInformation).Assembly,
        };
        return assemblies.ToArray();
    }
}