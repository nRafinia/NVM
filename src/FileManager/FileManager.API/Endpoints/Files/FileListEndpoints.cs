using FileManager.Application.FileLists.Models.GetFileContent;
using FileManager.Application.FileLists.Models.GetFileList;
using FileManager.Application.FileLists.Models.GetTextFileContent;
using Shared.Presentation.Extensions;

namespace FileManager.API.Endpoints.Files;

public static class FileListEndpoints
{
    private const string UserEndpointRoute = "/File";
    private const string UserEndpointTag = "File";

    public static WebApplication AddFileListEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(UserEndpointRoute).WithTags(UserEndpointTag);
        group.MapHttpGet<GetFileListRequest>("/List");
        group.MapHttpGet<GetFileContentRequest>("/Content");
        group.MapHttpGet<GetTextFileContentRequest>("/TextContent");
        
        return app;
    }
}