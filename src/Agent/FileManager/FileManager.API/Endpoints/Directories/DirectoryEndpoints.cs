using FileManager.Application.FileLists.Command.CreateFolder;
using FileManager.Application.FileLists.Command.DeleteFolder;
using Shared.Presentation.Extensions;

namespace FileManager.API.Endpoints.Directories;

public static class DirectoryEndpoints
{
    private const string DirectoryEndpointRoute = "/Directory";
    private const string DirectoryEndpointTag = "Directory";

    public static WebApplication AddDirectoryEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(DirectoryEndpointRoute).WithTags(DirectoryEndpointTag);
        group.MapHttpPost<CreateFolderCommand>("/");
        group.MapHttpDelete<DeleteFolderCommand>("/");
        
        return app;
    }
}