using Agent.UI.Infra.Models;
using Refit;

namespace Agent.UI.Infra.Interfaces;

public interface IServiceEndpoint
{
    [Get("/service")]
    Task<List<GetServicesResponse>> GetAvailableService();
}