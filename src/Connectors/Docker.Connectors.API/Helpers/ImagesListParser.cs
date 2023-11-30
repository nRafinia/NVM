using Connectors.Docker.Images;
using Docker.Connectors.API.Models;

namespace Docker.Connectors.API.Helpers;

public static class ImagesListParser
{
    public static IList<Image> Parse(IEnumerable<ImageRaw> rawImages)
    {
        return rawImages.Select(item => new Image(
                item.Id,
                item.RepoTags.First().Split(':', StringSplitOptions.RemoveEmptyEntries)[0],
                item.RepoTags.First().Split(':', StringSplitOptions.RemoveEmptyEntries)[1],
                DateTimeOffset.FromUnixTimeSeconds(item.Created).DateTime,
                item.Size,
                item.VirtualSize,
                item.SharedSize
            ))
            .ToList();
    }
}