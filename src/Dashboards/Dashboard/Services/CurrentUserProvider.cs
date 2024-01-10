using System.Security.Claims;
using SharedKernel.Abstractions;
using SharedKernel.ValueObjects;

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