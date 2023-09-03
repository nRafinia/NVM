using FileManager.Application.FileLists.Command.CreateFolder;
using FileManager.Application.FileLists.Command.DeleteFolder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Presentation.Extensions;

namespace FileManager.API.Endpoints.Directories;

public static class DirectoryEndpoints
{
    private const string DirectoryEndpointRoute = "/Directory";
    private const string DirectoryEndpointTag = "Directory";

    public static IEndpointRouteBuilder AddDirectoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(DirectoryEndpointRoute).WithTags(DirectoryEndpointTag);
        group.MapHttpPost<CreateFolderCommand>("/")
            .MapHttpDelete<DeleteFolderCommand>("/");

        return app;
    }
}