using Agent.UI.Application.Abstractions.Interfaces;
using Agent.UI.Domain.Models;
using Agent.UI.Infra.Interfaces;
using Microsoft.Extensions.Logging;
using Shared.Domain.Base.Results;
using Shared.Domain.Errors;

// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace Agent.UI.Infra.Services;

public class ServiceImp : IService
{
    private readonly IServiceEndpoint _service;
    private readonly ILogger<ServiceImp> _logger;

    public ServiceImp(IServiceEndpoint service, ILogger<ServiceImp> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<Result<IList<ServiceInformation>?>> GetAvailable()
    {
        try
        {
            var getServices = await _service.GetAvailableService();
            return getServices
                .Select(s => new ServiceInformation(s.Key, s.Name, s.Description))
                .ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<IList<ServiceInformation>>(SharedErrors.ProviderError);
        }
    }
}