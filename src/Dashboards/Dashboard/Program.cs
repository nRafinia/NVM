using Dashboard.Domain.Licenses;
using SharedKernel.Shared;

var licenseResponse = await LicenseManager.Load();
if (licenseResponse.IsFailure)
{
    Console.WriteLine("License has problem, please check it");
    Console.WriteLine("Error= {0}", licenseResponse.Error!.Message);
    return;
}

Console.WriteLine("License is valid");
Console.WriteLine(licenseResponse.Value!.ToString());

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.RegisterServices(
    typeof(Program).Assembly,
    typeof(Dashboard.Application.ConfigureServices).Assembly,
    typeof(Connectors.Docker.ConfigureServices).Assembly
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();