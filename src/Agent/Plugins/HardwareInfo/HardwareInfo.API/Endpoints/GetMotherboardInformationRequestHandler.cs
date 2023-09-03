using HardwareInfo.API.Models;
using HardwareInfo.Application.HardwareInformation;
using Microsoft.AspNetCore.Http;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace HardwareInfo.API.Endpoints;

public class GetMotherboardInformationRequestHandler : IHttpRequestHandler<GetMotherboardInformationRequest>
{
    private readonly IHardwareInformationLogic _hardwareInformation;

    public GetMotherboardInformationRequestHandler(IHardwareInformationLogic hardwareInformation)
    {
        _hardwareInformation = hardwareInformation;
    }

    public Task<IResult> Handle(GetMotherboardInformationRequest request, CancellationToken cancellationToken)
    {
        var response = _hardwareInformation.GetMotherboardList();
        return Task.FromResult(response.GetHttpResponse());
    }
}