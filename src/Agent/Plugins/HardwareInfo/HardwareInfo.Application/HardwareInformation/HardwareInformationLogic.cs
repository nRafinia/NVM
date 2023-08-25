using HardwareInfo.Application.Abstraction.Interfaces;
using HardwareInfo.Domain.Entities;
using Shared.Domain.Base.Results;

namespace HardwareInfo.Application.HardwareInformation;

public class HardwareInformationLogic : IHardwareInformationLogic
{
    private readonly IHardwareInformation _hardwareInformation;

    public HardwareInformationLogic(IHardwareInformation hardwareInformation)
    {
        _hardwareInformation = hardwareInformation;
    }

    public Result<OperationSystemInformation?> GetOperationSystem()
        => _hardwareInformation.GetOperationSystemInformation();


    public Result<MemoryInformation?> GetMemory()
        => _hardwareInformation.GetMemoryInformation();

    public Result<IList<BiosInformation>?> GetBiosList()
        => _hardwareInformation.GetBiosInformation();

    public Result<IList<CpuInformation>?> GetCpuList()
        => _hardwareInformation.GetCpusInformation();

    public Result<IList<MemoryHwInformation>?> GetMemorySlot()
        => _hardwareInformation.GetMemorySlotInformation();

    public Result<IList<MotherboardInformation>?> GetMotherboardList()
        => _hardwareInformation.GetMotherboardsInformation();

    public Result<IList<NetworkAdapterInformation>?> GetNetworkAdapterList()
        => _hardwareInformation.GetNetworkAdaptersInformation();
}