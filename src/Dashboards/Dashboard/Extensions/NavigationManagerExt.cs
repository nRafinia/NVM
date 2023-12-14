using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Dashboard.Extensions;

public static class NavigationManagerExt
{
    public static bool TryGetQueryString(this NavigationManager navigationManager, string key, out StringValues value)
    {
        var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
        var queryStrings = QueryHelpers.ParseQuery(uri.Query);
        return queryStrings.TryGetValue(key, out value);
    }  
}