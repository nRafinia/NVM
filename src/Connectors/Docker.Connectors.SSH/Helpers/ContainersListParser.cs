using System.Globalization;
using System.Text.Json;
using Connectors.Docker.Containers;
using Docker.Connectors.SSH.Models;

namespace Docker.Connectors.SSH.Helpers;

internal static class ContainersListParser
{
    public static IList<Container> Parse(string containerJsonText)
    {
        var jsonString = $"[{containerJsonText.Replace('\n', ',')}]";
        var containerListRaw = JsonSerializer.Deserialize<List<ContainerJsonRaw>>(jsonString);

        if (containerListRaw is null)
        {
            return new List<Container>(0);
        }

        var result = containerListRaw.Select(item => new Container(
            item.Id,
            DateTime.ParseExact(item.CreatedAt, "yyyy-MM-dd HH:mm:ss zzzz zzzz", CultureInfo.InvariantCulture,
                DateTimeStyles.None),
            item.Image,
            ParseLabels(item.Labels),
            item.Names.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
            item.Networks.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
            ParsePorts(item.Ports),
            item.State,
            item.Status
        )).ToList();

        return result;
    }

    private static Dictionary<string, string> ParseLabels(string labels)
    {
        var labelList = labels.Split(',', StringSplitOptions.RemoveEmptyEntries);

        return labelList
            .Select(label => label.Split('=', StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(labelPair => labelPair[0], labelPair => labelPair.Length>1?labelPair[1]:string.Empty);
    }

    private static List<Port> ParsePorts(string ports)
    {
        var portList = ports.Split(',', StringSplitOptions.RemoveEmptyEntries);

        return portList
            .Select(ParsePort).ToList();
    }

    private static Port ParsePort(string port)
    {
        var protocol = port.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var ipParts = protocol[0].Split(':', StringSplitOptions.RemoveEmptyEntries);
        var ports=ipParts[1].Split("->");
        return new Port(
            ipParts[0],
            int.Parse(ports[0]),
            int.Parse(ports[1]),
            protocol[1]
        );
    }
}