using Connectors.Docker.Containers;
using Docker.DotNet.Models;
using Port= Connectors.Docker.Containers.Port;

namespace Docker.Connectors.API.Helpers;

internal static class ContainersParser
{
    public static List<Container> List(IEnumerable<ContainerListResponse> serviceContainers)
    {
        return serviceContainers.Select(c => new Container(
            c.ID,
            c.Created,
            c.Image,
            c.Labels,
            c.Names,
            c.NetworkSettings.Networks.Select(n => n.Key).ToList(),
            c.Ports.Select(p=>new Port(p.IP,p.PrivatePort,p.PublicPort,p.Type)).ToList(),
            c.State,
            c.Status
        )).ToList();
    }
}