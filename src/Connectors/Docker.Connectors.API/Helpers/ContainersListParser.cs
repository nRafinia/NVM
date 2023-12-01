using Connectors.Docker.Containers;
using Docker.Connectors.API.Models;

namespace Docker.Connectors.API.Helpers;

internal static class ContainersListParser
{
    public static List<Container> Parse(IEnumerable<ContainerRaw> rawContainers)
    {
        return rawContainers.Select(c => new Container(
            c.Id,
            DateTimeOffset.FromUnixTimeSeconds(c.Created).DateTime,
            c.Image,
            c.Labels,
            c.Names,
            c.NetworkSettings.Networks.Select(n => n.Key).ToList(),
            c.Ports,
            c.State,
            c.Status
        )).ToList();
    }
}