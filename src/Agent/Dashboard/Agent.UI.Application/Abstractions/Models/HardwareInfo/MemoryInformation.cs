namespace Agent.UI.Application.Abstractions.Models.HardwareInfo;

public record MemoryInformation(ulong TotalPhysical, ulong AvailablePhysical, ulong UsagePercent);