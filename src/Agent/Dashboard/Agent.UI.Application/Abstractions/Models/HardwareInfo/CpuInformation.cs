namespace Agent.UI.Application.Abstractions.Models.HardwareInfo;

public record CpuInformation(string Name, string Manufacture, uint Cores, uint LogicalProcessors,
    uint L1InstructionCacheSize, uint L1DataCacheSize, uint L2CacheSize, uint L3CacheSize, ulong PercentProcessorTime,
    IList<ulong> CoreUsage);