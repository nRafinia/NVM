namespace HardwareInfo.Domain.Entities;

public record MemoryInformation(ulong TotalPhysical, ulong AvailablePhysical, ulong UsagePercent);