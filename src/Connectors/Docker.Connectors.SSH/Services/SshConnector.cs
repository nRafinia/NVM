using Connectors.Docker.Abstractions;
using Connectors.Docker.Containers;
using Connectors.Docker.Images;
using Docker.Connectors.SSH.Authentications;
using Docker.Connectors.SSH.Helpers;
using Renci.SshNet;

namespace Docker.Connectors.SSH.Services;

public class SshConnector : IConnector
{
    private readonly SshClient _client;

    public SshConnector(ISshAuthenticate authenticate)
    {
        var connection = authenticate.GetCredentials();
        _client = new SshClient(connection);
    }

    public Task<IList<Image>> GetImages(bool all = false)
    {
        var response = RunCommand(CommandGenerator.ListImages(all));
        var result = ImagesListParser.Parse(response);
        return Task.FromResult(result);
    }

    public Task<IList<Container>> GetContainers(bool all = false)
    {
        var response = RunCommand(CommandGenerator.ListContainers(all));
        var result = ContainersListParser.Parse(response);
        return Task.FromResult(result);
    }

    private string RunCommand(string command)
    {
        _client.Connect();
        using var cmd = _client.RunCommand(command);
        var result= cmd.Result;
        _client.Disconnect();
        return result;
    }
}