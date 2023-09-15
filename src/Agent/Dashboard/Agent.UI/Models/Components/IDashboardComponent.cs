using Agent.UI.Application.HardwareInfo;

namespace Agent.UI.Models.Components;

public interface IDashboardComponent
{
    Task GetInformation(IHardwareInformationLogic hardwareInformation);
    bool IsVisible { get; }
    string GetColSize { get; }
}