using System.Runtime.InteropServices;
using Hardware.Info;
using HardwareInfo.Application.Abstraction.Interfaces;
using HardwareInfo.Domain.Entities;
using HardwareInfo.Domain.Models;
using Microsoft.Extensions.Logging;
using Shared.Domain.Base.Results;
using Shared.Domain.Errors;

// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace HardwareInfo.Infra.Services;

public class HardwareInformation : IHardwareInformation
{
    private readonly ILogger<HardwareInformation> _logger;
    private readonly IHardwareInfo _hardwareInfo;

    public HardwareInformation(ILogger<HardwareInformation> logger, IHardwareInfo hardwareInfo)
    {
        _logger = logger;
        _hardwareInfo = hardwareInfo;
    }

    public Result<OperationSystemInformation?> GetOperationSystemInformation()
    {
        try
        {
            _hardwareInfo.RefreshOperatingSystem();

            var operationType = GetOperationType();
            var logicalDrives = new List<string>();
            if (operationType == OperationSystemType.Windows)
            {
                logicalDrives.AddRange(Environment.GetLogicalDrives());
            }
            else
            {
                logicalDrives.Add("/");
            }

            var result = new OperationSystemInformation(
                _hardwareInfo.OperatingSystem.Name,
                _hardwareInfo.OperatingSystem.VersionString,
                Environment.MachineName,
                Environment.UserName,
                Environment.Is64BitProcess,
                Environment.Is64BitOperatingSystem,
                operationType,
                logicalDrives
            );

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
        }

        return Result.Failure<OperationSystemInformation>(SharedErrors.InternalError);
    }

    public Result<MemoryInformation?> GetMemoryInformation()
    {
        try
        {
            _hardwareInfo.RefreshMemoryStatus();

            var total = _hardwareInfo.MemoryStatus.TotalPhysical;
            var available = _hardwareInfo.MemoryStatus.AvailablePhysical;
            var usage = 100 - (available * 100 / total);

            var result = new MemoryInformation(total, available, usage);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
        }

        return Result.Failure<MemoryInformation>(SharedErrors.InternalError);
    }

    public Result<IList<BiosInformation>?> GetBiosInformation()
    {
        try
        {
            _hardwareInfo.RefreshBIOSList();

            var result = _hardwareInfo.BiosList
                .Select(b => new BiosInformation(b.Manufacturer, b.Name, b.ReleaseDate, b.Version))
                .ToList();
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
        }

        return Result.Failure<IList<BiosInformation>>(SharedErrors.InternalError);
    }

    public Result<IList<CpuInformation>?> GetCpusInformation()
    {
        try
        {
            _hardwareInfo.RefreshCPUList();

            var result = _hardwareInfo.CpuList
                .Select(c => new CpuInformation(c.Name, c.Manufacturer, c.NumberOfCores, c.NumberOfLogicalProcessors,
                    c.L1InstructionCacheSize, c.L1DataCacheSize, c.L2CacheSize, c.L3CacheSize, c.PercentProcessorTime,
                    c.CpuCoreList.Select(cc => cc.PercentProcessorTime).ToList()))
                .ToList();
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
        }

        return Result.Failure<IList<CpuInformation>>(SharedErrors.InternalError);
    }

    public Result<IList<MemoryHwInformation>?> GetMemorySlotInformation()
    {
        try
        {
            _hardwareInfo.RefreshMemoryList();

            var result = _hardwareInfo.MemoryList
                .Select(m => new MemoryHwInformation(m.Capacity, m.Manufacturer, m.PartNumber))
                .ToList();
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
        }

        return Result.Failure<IList<MemoryHwInformation>>(SharedErrors.InternalError);
    }

    public Result<IList<MotherboardInformation>?> GetMotherboardsInformation()
    {
        try
        {
            _hardwareInfo.RefreshMotherboardList();

            var result = _hardwareInfo.MotherboardList
                .Select(m => new MotherboardInformation(m.Manufacturer, m.Product, m.SerialNumber))
                .ToList();
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
        }

        return Result.Failure<IList<MotherboardInformation>>(SharedErrors.InternalError);
    }

    public Result<IList<NetworkAdapterInformation>?> GetNetworkAdaptersInformation()
    {
        try
        {
            _hardwareInfo.RefreshNetworkAdapterList();

            var result = _hardwareInfo.NetworkAdapterList
                .Select(n => new NetworkAdapterInformation(
                    n.Name,
                    n.AdapterType,
                    n.MACAddress,
                    n.IPAddressList.Select(i => i.ToString()).ToList(),
                    n.DHCPServer.ToString(),
                    n.DNSServerSearchOrderList.Select(i => i.ToString()).ToList(),
                    n.IPSubnetList.Select(i => i.ToString()).ToList()))
                .ToList();
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
        }

        return Result.Failure<IList<NetworkAdapterInformation>>(SharedErrors.InternalError);
    }

    #region Private methods

    private static OperationSystemType GetOperationType()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return OperationSystemType.Windows;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return OperationSystemType.Linux;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return OperationSystemType.OSX;
        }

        return RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD)
            ? OperationSystemType.FreeBSD
            : OperationSystemType.Other;
    }

    #endregion
}