using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using Connectors.Docker.Networks;
using Docker.Connectors.SSH.Models;

namespace Docker.Connectors.SSH.Helpers;

public static class NetworksParser
{
    public static IList<Network> List(string networkJsonText)
    {
        if (string.IsNullOrWhiteSpace(networkJsonText))
        {
            return new List<Network>(0);
        }
        
        if (networkJsonText[^1] == '\n')
        {
            networkJsonText = networkJsonText[..^1];
        }

        var jsonString = $"[{networkJsonText.Replace('\n', ',')}]";
        var networkListRaw = JsonSerializer.Deserialize<List<NetworkJsonRaw>>(jsonString);

        if (networkListRaw is null)
        {
            return new List<Network>(0);
        }

        var result = networkListRaw.Select(item => new Network(
            item.Id,
            item.Name,
            item.Driver,
            item.Scope,
            item.Internal == "true",
            item.Labels
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(label => label.Split('='))
                .ToDictionary(label => label[0], label => label[1]),
            DateTimeOffset.ParseExact(
                RemoveMillisecondsFromString(item.CreatedAt), 
                "yyyy-MM-dd HH:mm:ss zzzz",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None).DateTime,
            item.IPv6 == "true"
        )).ToList();

        return result;
    }


    private static string RemoveMillisecondsFromString(string timeString)
    {
        // Check the format matches expected pattern
        if (!Regex.IsMatch(timeString, @"^\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}\.\d+ [+\-]\d{4} [+\-]\d{4}$"))
        {
            throw new ArgumentException("Invalid time format");
        }

        // Extract date and time parts
        var dateAndTime = timeString.Substring(0, timeString.IndexOf('.'));

        // Combine with timezone information
        return $"{dateAndTime} {timeString.Split(' ',StringSplitOptions.RemoveEmptyEntries).Last()}";
    }
}