using Agent.UI.Domain.Models;

namespace Agent.UI.Infra.Handlers;

public class ServiceEndpointHandler : DelegatingHandler
{
    public const string FakeBaseAddress = "http://fake.to.replace";

    private readonly TargetServiceDomain _target;

    public ServiceEndpointHandler(TargetServiceDomain target)
    {
        _target = target;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var relativeUrl = request.RequestUri!.AbsoluteUri[FakeBaseAddress.Length..];
        var newUri = Combine(_target.Domain, relativeUrl);
        request.RequestUri = new Uri(newUri);
        return await base.SendAsync(request, cancellationToken);
    }

    private static string Combine(string baseUrl, string relativeUrl)
        => $"{baseUrl.TrimEnd('/')}/{relativeUrl.TrimStart('/')}";
}