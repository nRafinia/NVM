using Agent.UI.Application.Abstractions.Models;
using Refit;

namespace Agent.UI.Infra.Interfaces;

public interface IServiceEndpoint
{
    [Get("/service")]
    Task<List<ServiceInformation>> GetAvailableService();
}