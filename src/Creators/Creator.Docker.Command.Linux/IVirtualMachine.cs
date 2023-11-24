using Creator.Shared.Models;

namespace Creator.Docker.Command.Linux;

public interface IVirtualMachine
{
    IList<NetworkInterfaces> GetNetworkInterfaces();
    IList<Container> GetContainers();
    IList<Image> GetImages();
}