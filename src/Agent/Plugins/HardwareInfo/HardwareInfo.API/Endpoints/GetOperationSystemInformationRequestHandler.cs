using HardwareInfo.API.Models;
using HardwareInfo.Application.HardwareInformation;
using Microsoft.AspNetCore.Http;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace HardwareInfo.API.Endpoints;

public class GetOperationSystemInformationRequestHandler : IHttpRequestHandler<GetOperationSystemInformationRequest>
{
    private readonly IHardwareInformationLogic _hardwareInformation;

    public GetOperationSystemInformationRequestHandler(IHardwareInformationLogic hardwareInformation)
    {
        _hardwareInformation = hardwareInformation;
    }

    public Task<IResult> Handle(GetOperationSystemInformationRequest request, CancellationToken cancellationToken)
    {
        var response = _hardwareInformation.GetOperationSystem();
        return Task.FromResult(response.GetHttpResponse());
    }
}