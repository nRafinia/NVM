using Agent.UI.Application.Abstractions.Interfaces;
using Agent.UI.Application.Abstractions.Models.HardwareInfo;
using Agent.UI.Infra.Interfaces;
using Microsoft.Extensions.Logging;
using Shared.Domain.Base.Results;
using Shared.Domain.Errors;

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
}