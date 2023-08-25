using HardwareInfo.Domain.Entities;
using Shared.Domain.Base.Results;

namespace HardwareInfo.Application.Abstraction.Interfaces;

public interface IHardwareInformation
{
    Result<OperationSystemInformation?> GetOperationSystemInformation();
    Result<MemoryInformation?> GetMemoryInformation();
    Result<IList<BiosInformation>?> GetBiosInformation();
    Result<IList<CpuInformation>?> GetCpusInformation();
    Result<IList<MemoryHwInformation>?> GetMemorySlotInformation();
    Result<IList<MotherboardInformation>?> GetMotherboardsInformation();
    Result<IList<NetworkAdapterInformation>?> GetNetworkAdaptersInformation();
}