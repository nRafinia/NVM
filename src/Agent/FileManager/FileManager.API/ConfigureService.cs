using FileManager.API.Endpoints.Directories;
using FileManager.API.Endpoints.Files;
using FileManager.API.Endpoints.Paths;
using FileManager.API.Middlewares;

namespace FileManager.API;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        Shared.Presentation.ConfigureServices.AddSharedPresentationServices(services, configuration);

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static void AddPipelines(this WebApplication app)
    {
        //if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<UnhandledExceptionMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        app
            .AddPathEndpoints()
            .AddDirectoryEndpoints()
            .AddFileEndpoints();
    }
}