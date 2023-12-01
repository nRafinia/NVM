using Connectors.Docker.Abstractions;
using Connectors.Docker.Containers;
using Connectors.Docker.Images;
using Docker.Connectors.API.Authentications;
using Docker.Connectors.API.Helpers;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Docker.Connectors.API.Services;

public class ApiConnector : IConnector
{
    private readonly DockerClient _client;

    public ApiConnector(IApiAuthenticate authenticate, Uri endpoint)
    {
        _client = new DockerClientConfiguration(endpoint, authenticate.GetCredentials()).CreateClient();
    }

    public async Task<IList<Image>> GetImages(bool all = false)
    {
        var images = await _client.Images.ListImagesAsync(new ImagesListParameters()
        {
            All = all
        });
        
        return ImagesListParser.Parse(images);
    }

    public async Task<IList<Container>> GetContainers(bool all = false)
    {
        var containers = await _client.Containers.ListContainersAsync(new ContainersListParameters()
        {
            All = all
        });

        return ContainersListParser.Parse(containers);
    }
}