using System.Security.Claims;
using Dashboard.Domain.ValueObjects;
using SharedKernel.Abstractions;

namespace Dashboard.Services;

public class CurrentUserProvider(IHttpContextAccessor httpContext) : ICurrentUser
{
    public IdColumn GetUserId()
    {
        if (!(httpContext.HttpContext?.User.Identity?.IsAuthenticated ?? false))
        {
            return IdColumn.None;
        }

        return httpContext.HttpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier)?.Value ??
               IdColumn.None;
    }
}