using FileManager.Application.FileLists.Command.DeleteFile;
using FileManager.Application.FileLists.Command.WriteToBinaryFile;
using FileManager.Application.FileLists.Command.WriteToTextFile;
using FileManager.Application.FileLists.Query.GetFileContent;
using FileManager.Application.FileLists.Query.GetTextFileContent;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Presentation.Extensions;

namespace FileManager.API.Endpoints.Files;

public static class FileEndpoints
{
    private const string UserEndpointRoute = "/File";
    private const string UserEndpointTag = "File";

    public static IEndpointRouteBuilder AddFileEndpoints(this IEndpointRouteBuilder app, string parentTag)
    {
        var group = app.MapGroup(UserEndpointRoute).WithTags($"{parentTag} - {UserEndpointTag}");
        group.MapHttpGet<GetFileContentQuery>("/Content");
        group.MapHttpGet<GetTextFileContentQuery>("/TextContent");
        group.MapHttpDelete<DeleteFileCommand>("/");
        group.MapHttpPut<WriteToBinaryFileCommand>("/Write");
        group.MapHttpPut<WriteToTextFileCommand>("/WriteText");
        
        return app;
    }
}