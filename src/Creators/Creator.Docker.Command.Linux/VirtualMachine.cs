using System.Globalization;
using Creator.Docker.Command.Linux.Shared;
using Creator.Shared.Models;
using Microsoft.Extensions.Logging;

namespace Creator.Docker.Command.Linux;

public class VirtualMachine : IVirtualMachine
{
    private readonly ILogger<VirtualMachine> _logger;

    public VirtualMachine(ILogger<VirtualMachine> logger)
    {
        _logger = logger;
    }

    public IList<NetworkInterfaces> GetNetworkInterfaces()
    {
        var commandRunner = new CommandRunner(_logger);
        var result = commandRunner.Run("docker", "network ls --format \"{{.ID}}|{{.Name}}|{{.Scope}}|{{.Driver}}\"");

        if (result.IsFailure)
        {
            _logger.LogError("Error getting network interfaces");
            return new List<NetworkInterfaces>();
        }

        var networkInterfaces = result.Value!
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split('|'))
            .Select(x => new NetworkInterfaces(x[0], x[1], x[2], x[3]))
            .ToList();

        return networkInterfaces;
    }

    public IList<Container> GetContainers()
    {
        var commandRunner = new CommandRunner(_logger);
        var result = commandRunner.Run("docker",
            "ps --format \"{{.ID}}|{{.Image}}|{{.CreatedAt}}|{{.RunningFor}}|{{.Ports}}|{{.Status}}|{{.Size}}|{{.Names}}\"");

        if (result.IsFailure)
        {
            _logger.LogError("Error getting containers");
            return new List<Container>();
        }

        var containers = result.Value!
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split('|'))
            .Select(x => new Container(
                x[0],
                x[1],
                DateTime.ParseExact(x[2], "yyyy-MM-dd HH:mm:ss zzzz zzzz", CultureInfo.InvariantCulture,
                    DateTimeStyles.None),
                x[3],
                x[4],
                x[5],
                x[6],
                x[7]))
            .ToList();

        return containers;
    }
    
    public IList<Image> GetImages()
    {
        var commandRunner = new CommandRunner(_logger);
        var result = commandRunner.Run("docker",
            "images --format \"{{.ID}}|{{.Repository}}|{{.Tag}}|{{.Digest}}|{{.CreatedAt}}|{{.Size}}\"");

        if (result.IsFailure)
        {
            _logger.LogError("Error getting images");
            return new List<Image>();
        }

        var images = result.Value!
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split('|'))
            .Select(x => new Image(
                x[0],
                x[1],
                x[2],
                x[3],
                DateTime.ParseExact(x[4], "yyyy-MM-dd HH:mm:ss zzzz zzzz", CultureInfo.InvariantCulture,
                    DateTimeStyles.None),
                x[5]))
            .ToList();

        return images;
    }
}