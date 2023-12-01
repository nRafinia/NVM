using Docker.DotNet.Models;
using Network = Connectors.Docker.Networks.Network;

namespace Docker.Connectors.API.Helpers;

public static class NetworksParser
{
    public static IList<Network> List(IEnumerable<NetworkResponse> serviceNetwork)
    {
        return serviceNetwork.Select(n => new Network(
            n.ID,
            n.Name,
            n.Driver,
            n.Scope,
            n.Internal,
            n.Labels,
            n.Created,
            n.EnableIPv6
        )).ToList();
    }
}