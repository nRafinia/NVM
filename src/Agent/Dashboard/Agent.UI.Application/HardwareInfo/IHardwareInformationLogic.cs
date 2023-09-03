using Agent.UI.Application.Abstractions.Models.HardwareInfo;
using Shared.Domain.Base.Results;

namespace Agent.UI.Application.HardwareInfo;

public interface IHardwareInformationLogic
{
    Task<Result<OperationSystemInformation?>> GetOs();
}