using Agent.UI.Application.Abstractions.Interfaces;
using Agent.UI.Application.Abstractions.Models.HardwareInfo;
using Shared.Domain.Base.Results;

namespace Agent.UI.Application.HardwareInfo;

public class HardwareInformationLogic : IHardwareInformationLogic
{
    private readonly IHardwareInformation _hardwareInformation;

    public HardwareInformationLogic(IHardwareInformation hardwareInformation)
    {
        _hardwareInformation = hardwareInformation;
    }

    public Task<Result<OperationSystemInformation?>> GetOs()
    {
        return _hardwareInformation.GetOs();
    }
}