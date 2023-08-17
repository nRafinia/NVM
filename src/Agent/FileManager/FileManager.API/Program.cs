using System.Reflection;
using FileManager.API;
using FileManager.Application;
using FileManager.Domain;
using FileManager.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assemblies = new List<Assembly>()
{
    typeof(Shared.Domain.ConfigureServices).Assembly,
    typeof(FileManager.Domain.ConfigureServices).Assembly,
    typeof(Shared.Infra.ConfigureServices).Assembly,
    typeof(FileManager.Infra.ConfigureServices).Assembly,
    typeof(Shared.Application.ConfigureServices).Assembly,
    typeof(FileManager.Application.ConfigureServices).Assembly,
    typeof(Program).Assembly,
    typeof(Shared.Presentation.ConfigureServices).Assembly,
};

builder.Services
    .AddDomainServices()
    .AddInfraServices(builder.Configuration)
    .AddApplicationServices(assemblies.ToArray())
    .AddPresentationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.AddPipelines();

app.Run();