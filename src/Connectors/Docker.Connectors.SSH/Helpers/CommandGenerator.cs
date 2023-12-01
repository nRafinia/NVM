namespace Docker.Connectors.SSH.Helpers;

internal static class CommandGenerator
{
    public static string ListImages(bool all)
    {
        return $"docker images {(all ? " -a" : string.Empty)} --format='{{{{json .}}}}'";
    }    
    
    public static string ListContainers(bool all)
    {
        return $"docker ps {(all ? " -a" : string.Empty)} --format='{{{{json .}}}}'";
    }
}