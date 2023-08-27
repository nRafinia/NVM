using Agent.UI.Application.Abstractions.Interfaces;
using Agent.UI.Application.Abstractions.Models;
using Agent.UI.Application.Services.Models;
using Shared.Domain.Base.Results;

namespace Agent.UI.Application.Services;

public class ServiceLogic : IServiceLogic
{
    private readonly IService _service;

    public ServiceLogic(IService service)
    {
        _service = service;
    }

    public async Task<Result<IList<AvailableServiceResponse>?>> GetAvailable()
    {
        var availableServicesResponse = await _service.GetAvailable();
        if (availableServicesResponse.IsFailure)
        {
            return Result.Failure<IList<AvailableServiceResponse>>(availableServicesResponse.Error!);
        }

        var availableServices = availableServicesResponse.Value!;

        return GetOrderedAvailableService(availableServices).ToList();
    }

    private IEnumerable<AvailableServiceResponse> GetOrderedAvailableService(IList<ServiceInformation> services)
    {
        var service = services.FirstOrDefault(s => s.Key == ServiceConst.HardwareInfo);
        if (service is not null)
        {
            yield return new AvailableServiceResponse(1, service.Key, service.Name, service.Description);
        }

        service = services.FirstOrDefault(s => s.Key == ServiceConst.FileManager);
        if (service is not null)
        {
            yield return new AvailableServiceResponse(2, service.Key, service.Name, service.Description);
        }
    }
}