using Agent.UI;
using Agent.UI.Application;
using Agent.UI.Domain;
using Agent.UI.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDomainServices()
    .AddApplicationServices()
    .AddInfraServices(builder.Configuration)
    .AddPresentationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.AddPipelines();

app.Run();