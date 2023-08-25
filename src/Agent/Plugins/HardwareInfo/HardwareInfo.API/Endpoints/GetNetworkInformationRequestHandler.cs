using HardwareInfo.API.Models;
using HardwareInfo.Application.HardwareInformation;
using Microsoft.AspNetCore.Http;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace HardwareInfo.API.Endpoints;

public class GetNetworkInformationRequestHandler : IHttpRequestHandler<GetNetworkInformationRequest>
{
    private readonly IHardwareInformationLogic _hardwareInformation;

    public GetNetworkInformationRequestHandler(IHardwareInformationLogic hardwareInformation)
    {
        _hardwareInformation = hardwareInformation;
    }

    public Task<IResult> Handle(GetNetworkInformationRequest request, CancellationToken cancellationToken)
    {
        var response = _hardwareInformation.GetNetworkAdapterList();
        return Task.FromResult(response.GetHttpResponse());
    }
}