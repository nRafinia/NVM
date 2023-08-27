using Agent.UI.Application.Abstractions.Models.HardwareInfo;
using Refit;

namespace Agent.UI.Infra.Interfaces;

public interface IHardwareInformationEndpoint
{
    [Get("/hardwareInfo/OS")]
    Task<OperationSystemInformation> GetOs();
}