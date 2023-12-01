namespace Docker.Connectors.SSH.Helpers;

internal static class CommandGenerator
{
    public static string ListImages(bool all)
        => $"docker images {(all ? " -a" : string.Empty)} --format='{{{{json .}}}}'";


    public static string ListContainers(bool all)
        => $"docker ps {(all ? " -a" : string.Empty)} --format='{{{{json .}}}}'";    
    
    public static string ListNetworks()
        => $"docker network ls --format='{{{{json .}}}}'";
}