using Agent.UI.Application.Abstractions.Models.HardwareInfo;
using Shared.Domain.Base.Results;

namespace Agent.UI.Application.HardwareInfo;

public interface IHardwareInformationLogic
{
    Task<Result<OperationSystemInformation?>> GetOs();
    Task<Result<MemoryInformation?>> GetMemory();
    Task<Result<IList<BiosInformation>?>> GetBios();
    Task<Result<IList<CpuInformation>?>> GetCpu();
    Task<Result<IList<MemoryHwInformation>?>> GetMemorySlot();
    Task<Result<IList<MotherboardInformation>?>> GetMotherboard();
    Task<Result<IList<NetworkAdapterInformation>?>> GetNetworkAdapter();
}