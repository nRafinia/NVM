using HardwareInfo.API.Models;
using HardwareInfo.Application.HardwareInformation;
using Microsoft.AspNetCore.Http;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace HardwareInfo.API.Endpoints;

public class GetCpuInformationRequestHandler : IHttpRequestHandler<GetCpuInformationRequest>
{
    private readonly IHardwareInformationLogic _hardwareInformation;

    public GetCpuInformationRequestHandler(IHardwareInformationLogic hardwareInformation)
    {
        _hardwareInformation = hardwareInformation;
    }

    public Task<IResult> Handle(GetCpuInformationRequest request, CancellationToken cancellationToken)
    {
        var response = _hardwareInformation.GetCpuList();
        return Task.FromResult(response.GetHttpResponse());
    }
}