using HardwareInfo.API.Models;
using HardwareInfo.Application.HardwareInformation;
using Microsoft.AspNetCore.Http;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace HardwareInfo.API.Endpoints;

public class GetBiosInformationRequestHandler : IHttpRequestHandler<GetBiosInformationRequest>
{
    private readonly IHardwareInformationLogic _hardwareInformation;

    public GetBiosInformationRequestHandler(IHardwareInformationLogic hardwareInformation)
    {
        _hardwareInformation = hardwareInformation;
    }

    public Task<IResult> Handle(GetBiosInformationRequest request, CancellationToken cancellationToken)
    {
        var response = _hardwareInformation.GetBiosList();
        return Task.FromResult(response.GetHttpResponse());
    }
}