using HardwareInfo.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Presentation.Extensions;

namespace HardwareInfo.API.Endpoints;

public static class HardwareInformationEndpoints
{
    private const string HwEndpointRoute = "/";
    private const string HwEndpointTag = "Hardware information";

    public static IEndpointRouteBuilder AddHardwareInformationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(HwEndpointRoute).WithTags(HwEndpointTag);
        group.MapHttpGet<GetBiosInformationRequest>("/Bios");
        group.MapHttpGet<GetCpuInformationRequest>("/Cpu");
        group.MapHttpGet<GetMemorySlotInformationRequest>("/MemorySlot");
        group.MapHttpGet<GetMemoryInformationRequest>("/Memory");
        group.MapHttpGet<GetMotherboardInformationRequest>("/Motherboard");
        group.MapHttpGet<GetNetworkInformationRequest>("/Network");
        group.MapHttpGet<GetOperationSystemInformationRequest>("/OS");

        return app;
    }
}
