using FileManager.Application.FileLists.Models;
using Shared.Presentation.Extensions;

namespace FileManager.API.Endpoints.FileLists;

public static class FileListEndpoints
{
    private const string UserEndpointRoute = "/filelist";
    private const string UserEndpointTag = "filelist";

    public static WebApplication AddFileListEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(UserEndpointRoute).WithTags(UserEndpointTag);
        group.MapHttpGet<GetFileListRequest>("/");
        
        return app;
    }
}