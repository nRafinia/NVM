using Agent.UI.Application.Abstractions.Interfaces;
using Agent.UI.Application.Abstractions.Models.HardwareInfo;
using Shared.Domain.Base.Results;

namespace Agent.UI.Application.HardwareInfo;

public class HardwareInformationLogic : IHardwareInformationLogic
{
    private readonly IHardwareInformation _hardwareInformation;

    public HardwareInformationLogic(IHardwareInformation hardwareInformation)
    {
        _hardwareInformation = hardwareInformation;
    }

    public Task<Result<OperationSystemInformation?>> GetOs()
        => _hardwareInformation.GetOs();    
    
    public Task<Result<MemoryInformation?>> GetMemory()
        => _hardwareInformation.GetMemory();    
    
    public Task<Result<IList<BiosInformation>?>> GetBios()
        => _hardwareInformation.GetBios();    
    
    public Task<Result<IList<CpuInformation>?>> GetCpu()
        => _hardwareInformation.GetCpu();    
    
    public Task<Result<IList<MemoryHwInformation>?>> GetMemorySlot()
        => _hardwareInformation.GetMemorySlot();    
    
    public Task<Result<IList<MotherboardInformation>?>> GetMotherboard()
        => _hardwareInformation.GetMotherboard();    
    
    public Task<Result<IList<NetworkAdapterInformation>?>> GetNetworkAdapter()
        => _hardwareInformation.GetNetworkAdapter();
}