using Connectors.Docker.Abstractions;

namespace Connectors.Docker.Images;

public interface IImageService
{
    public Task<IList<Image>> GetList(IConnector connector, bool all = false);
}