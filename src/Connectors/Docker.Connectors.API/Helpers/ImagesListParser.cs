using Connectors.Docker.Images;
using Docker.DotNet.Models;

namespace Docker.Connectors.API.Helpers;

internal static class ImagesListParser
{
    public static IList<Image> Parse(IEnumerable<ImagesListResponse> serviceImages)
    {
        return serviceImages.Select(item => new Image(
                item.ID,
                item.RepoTags.First().Split(':', StringSplitOptions.RemoveEmptyEntries)[0],
                item.RepoTags.First().Split(':', StringSplitOptions.RemoveEmptyEntries)[1],
                item.Created,
                item.Size,
                item.VirtualSize,
                item.SharedSize
            ))
            .ToList();
    }
}