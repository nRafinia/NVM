using HardwareInfo.Domain.Models;

namespace HardwareInfo.Domain.Entities;

public record OperationSystemInformation(string Name, string Version, string MachineName, string UserName, bool Isx64Processor, 
    bool Isx64, OperationSystemType Type, IList<string> LogicalDrives);