using Agent.UI.Application.Abstractions.Models.HardwareInfo;
using Shared.Domain.Base.Results;

namespace Agent.UI.Application.Abstractions.Interfaces;

public interface IHardwareInformation
{
    Task<Result<OperationSystemInformation?>> GetOs();
}