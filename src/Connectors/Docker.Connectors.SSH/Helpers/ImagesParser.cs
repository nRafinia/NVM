using System.Globalization;
using System.Text.Json;
using Common;
using Connectors.Docker.Images;
using Docker.Connectors.SSH.Models;

namespace Docker.Connectors.SSH.Helpers;

public static class ImagesParser
{
    internal static IList<Image> List(string imageJsonText)
    {
        if (imageJsonText[^1] == '\n')
        {
            imageJsonText = imageJsonText[..^1];
        }
        
        var jsonString = $"[{imageJsonText.Replace('\n', ',')}]";
        var imageListRaw = JsonSerializer.Deserialize<List<ImageJsonRaw>>(jsonString);

        if (imageListRaw is null)
        {
            return new List<Image>(0);
        }

        var result = imageListRaw.Select(item => new Image(
            item.Id,
            item.Repository,
            item.Tag,
            DateTime.ParseExact(item.Created, "yyyy-MM-dd HH:mm:ss zzzz zzzz", CultureInfo.InvariantCulture,
                DateTimeStyles.None),
            item.Size == "N/A" ? -1 : SizeConverter.ConvertStringToBytes(item.Size),
            item.VirtualSize == "N/A" ? -1 : SizeConverter.ConvertStringToBytes(item.VirtualSize),
            item.SharedSize == "N/A" ? -1 : SizeConverter.ConvertStringToBytes(item.SharedSize)
        )).ToList();

        return result;
    }
}