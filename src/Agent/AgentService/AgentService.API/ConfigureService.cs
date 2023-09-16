using AgentService.Application.Plugins;
using Microsoft.OpenApi.Models;
using Shared.Presentation.Middlewares;

namespace AgentService.API;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        Shared.Presentation.ConfigureServices.AddSharedPresentationServices(services, configuration);

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(generator =>
        {
            foreach (var plugin in PluginCollection.GetPlugins)
            {
                generator.SwaggerDoc(plugin.Key, new OpenApiInfo()
                {
                    Title = plugin.Name,
                    Description = plugin.Description
                });
            }
        });

        return services;
    }

    public static void AddPipelines(this WebApplication app)
    {
        //if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                foreach (var plugin in PluginCollection.GetPlugins)
                {
                    s.SwaggerEndpoint($"/swagger/{plugin.Key}/swagger.json", plugin.Name);
                }
            });
        }

        //app.UseHttpsRedirection();

        app.UseMiddleware<UnhandledExceptionMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

    }
}