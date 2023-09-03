using HardwareInfo.API.Models;
using HardwareInfo.Application.HardwareInformation;
using Microsoft.AspNetCore.Http;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace HardwareInfo.API.Endpoints;

public class GetMemorySlotInformationRequestHandler : IHttpRequestHandler<GetMemorySlotInformationRequest>
{
    private readonly IHardwareInformationLogic _hardwareInformation;

    public GetMemorySlotInformationRequestHandler(IHardwareInformationLogic hardwareInformation)
    {
        _hardwareInformation = hardwareInformation;
    }

    public Task<IResult> Handle(GetMemorySlotInformationRequest request, CancellationToken cancellationToken)
    {
        var response = _hardwareInformation.GetMemorySlot();
        return Task.FromResult(response.GetHttpResponse());
    }
}