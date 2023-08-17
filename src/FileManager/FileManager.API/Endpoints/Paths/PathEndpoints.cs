using FileManager.Application.FileLists.Query.GetFileList;
using Shared.Presentation.Extensions;

namespace FileManager.API.Endpoints.Paths;

public static class PathEndpoints
{
    private const string PathEndpointRoute = "/Path";
    private const string PathEndpointTag = "Path";

    public static WebApplication AddPathEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(PathEndpointRoute).WithTags(PathEndpointTag);
        group.MapHttpGet<GetFileListQuery>("/");
        
        return app;
    }
}