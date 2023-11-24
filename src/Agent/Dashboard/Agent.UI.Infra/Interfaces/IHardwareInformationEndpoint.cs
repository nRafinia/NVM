using Agent.UI.Application.Abstractions.Models.HardwareInfo;
using Refit;

namespace Agent.UI.Infra.Interfaces;

public interface IHardwareInformationEndpoint
{
    [Get("/hardwareInfo/OS")]
    Task<OperationSystemInformation> GetOs();
    
    [Get("/hardwareInfo/Memory")]
    Task<MemoryInformation> GetMemory();
    
    [Get("/hardwareInfo/Bios")]
    Task<IList<BiosInformation>> GetBios();
    
    [Get("/hardwareInfo/Cpu")]
    Task<IList<CpuInformation>> GetCpu();
    
    [Get("/hardwareInfo/MemorySlot")]
    Task<IList<MemoryHwInformation>> GetMemorySlot();
    
    [Get("/hardwareInfo/Motherboard")]
    Task<IList<MotherboardInformation>> GetMotherboard();
    
    [Get("/hardwareInfo/Network")]
    Task<IList<NetworkAdapterInformation>> GetNetworkAdapter();
    
}