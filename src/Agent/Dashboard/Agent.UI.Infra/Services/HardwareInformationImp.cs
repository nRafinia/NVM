using Agent.UI.Application.Abstractions.Interfaces;
using Agent.UI.Application.Abstractions.Models.HardwareInfo;
using Agent.UI.Infra.Interfaces;
using Microsoft.Extensions.Logging;
using Shared.Domain.Base.Results;
using Shared.Domain.Errors;
// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace Agent.UI.Infra.Services;

public class HardwareInformationImp : IHardwareInformation
{
    private readonly IHardwareInformationEndpoint _service;
    private readonly ILogger<HardwareInformationImp> _logger;

    public HardwareInformationImp(IHardwareInformationEndpoint service, ILogger<HardwareInformationImp> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<Result<OperationSystemInformation?>> GetOs()
    {
        try
        {
            return await _service.GetOs();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<OperationSystemInformation>(SharedErrors.ProviderError);
        }
    }

    public async Task<Result<MemoryInformation?>> GetMemory()
    {
        try
        {
            return await _service.GetMemory();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<MemoryInformation>(SharedErrors.ProviderError);
        }
    }

    public async Task<Result<IList<BiosInformation>?>> GetBios()
    {
        try
        {
            var result = await _service.GetBios();
            return Result.Success<IList<BiosInformation>?>(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<IList<BiosInformation>>(SharedErrors.ProviderError);
        }
    }    
    
    public async Task<Result<IList<CpuInformation>?>> GetCpu()
    {
        try
        {
            var result = await _service.GetCpu();
            return Result.Success<IList<CpuInformation>?>(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<IList<CpuInformation>>(SharedErrors.ProviderError);
        }
    }    
    
    public async Task<Result<IList<MemoryHwInformation>?>> GetMemorySlot()
    {
        try
        {
            var result = await _service.GetMemorySlot();
            return Result.Success<IList<MemoryHwInformation>?>(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<IList<MemoryHwInformation>>(SharedErrors.ProviderError);
        }
    }    
    
    public async Task<Result<IList<MotherboardInformation>?>> GetMotherboard()
    {
        try
        {
            var result = await _service.GetMotherboard();
            return Result.Success<IList<MotherboardInformation>?>(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<IList<MotherboardInformation>>(SharedErrors.ProviderError);
        }
    }    
    
    public async Task<Result<IList<NetworkAdapterInformation>?>> GetNetworkAdapter()
    {
        try
        {
            var result = await _service.GetNetworkAdapter();
            return Result.Success<IList<NetworkAdapterInformation>?>(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<IList<NetworkAdapterInformation>>(SharedErrors.ProviderError);
        }
    }
}