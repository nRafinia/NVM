using HardwareInfo.API.Models;
using HardwareInfo.Application.HardwareInformation;
using Microsoft.AspNetCore.Http;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace HardwareInfo.API.Endpoints;

public class GetMemoryInformationRequestHandler : IHttpRequestHandler<GetMemoryInformationRequest>
{
    private readonly IHardwareInformationLogic _hardwareInformation;

    public GetMemoryInformationRequestHandler(IHardwareInformationLogic hardwareInformation)
    {
        _hardwareInformation = hardwareInformation;
    }

    public Task<IResult> Handle(GetMemoryInformationRequest request, CancellationToken cancellationToken)
    {
        var response = _hardwareInformation.GetMemory();
        return Task.FromResult(response.GetHttpResponse());
    }
}