using FileManager.Application.FileLists.Query.GetFileList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Presentation.Extensions;

namespace FileManager.API.Endpoints.Paths;

public static class PathEndpoints
{
    private const string PathEndpointRoute = "/Path";
    private const string PathEndpointTag = "Path";

    public static IEndpointRouteBuilder AddPathEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(PathEndpointRoute).WithTags(PathEndpointTag);
        group.MapHttpGet<GetFileListQuery>("/");
        
        return app;
    }
}