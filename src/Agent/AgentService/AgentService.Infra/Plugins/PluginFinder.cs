using System.Reflection;

namespace AgentService.Infra.Plugins;

public static class PluginFinder
{
    public static Assembly[] GetList()
    {
        return Array.Empty<Assembly>();
    }
}