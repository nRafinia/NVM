using HardwareInfo.Domain.Entities;
using Shared.Domain.Base.Results;

namespace HardwareInfo.Application.HardwareInformation;

public interface IHardwareInformationLogic
{
    Result<OperationSystemInformation?> GetOperationSystem();
    Result<MemoryInformation?> GetMemory();
    Result<IList<BiosInformation>?> GetBiosList();
    Result<IList<CpuInformation>?> GetCpuList();
    Result<IList<MemoryHwInformation>?> GetMemorySlot();
    Result<IList<MotherboardInformation>?> GetMotherboardList();
    Result<IList<NetworkAdapterInformation>?> GetNetworkAdapterList();
}