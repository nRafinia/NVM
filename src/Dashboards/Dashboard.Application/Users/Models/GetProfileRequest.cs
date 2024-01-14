using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Models;

public record GetProfileRequest(IdColumn UserId, string UserName);