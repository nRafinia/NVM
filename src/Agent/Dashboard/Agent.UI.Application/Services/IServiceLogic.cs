using Agent.UI.Application.Services.Models;
using Shared.Domain.Base.Results;

namespace Agent.UI.Application.Services;

public interface IServiceLogic
{
    Task<Result<IList<AvailableServiceResponse>?>> GetAvailable();
}