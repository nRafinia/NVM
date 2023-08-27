using Agent.UI.Domain.Models;
using Shared.Domain.Base.Results;

namespace Agent.UI.Application.Abstractions.Interfaces;

public interface IService
{
    Task<Result<IList<ServiceInformation>?>> GetAvailable();
}