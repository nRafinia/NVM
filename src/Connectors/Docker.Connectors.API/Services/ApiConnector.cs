using Connectors.Docker.Abstractions;
using Connectors.Docker.Containers;
using Connectors.Docker.Images;
using Docker.Connectors.API.Authentications;
using Docker.Connectors.API.Helpers;
using Docker.DotNet;
using Docker.DotNet.Models;
using Network = Connectors.Docker.Networks.Network;

namespace Docker.Connectors.API.Services;

public class ApiConnector : IConnector
{
    private readonly DockerClient _client;

    public ApiConnector(IApiAuthenticate authenticate)
    {
        _client = authenticate.GetCredentials().CreateClient();
    }

    public async Task<IList<Image>> GetImages(bool all = false)
    {
        var images = await _client.Images.ListImagesAsync(new ImagesListParameters()
        {
            All = all
        });
        
        return ImagesParser.List(images);
    }

    public async Task<IList<Container>> GetContainers(bool all = false)
    {
        var containers = await _client.Containers.ListContainersAsync(new ContainersListParameters()
        {
            All = all
        });

        return ContainersParser.List(containers);
    }    
    
    public async Task<IList<Network>> GetNetworks()
    {
        var containers = await _client.Networks.ListNetworksAsync();

        return NetworksParser.List(containers);
    }
}