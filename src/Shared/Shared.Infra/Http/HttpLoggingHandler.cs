using System.Diagnostics;
using System.Net.Http.Headers;

namespace Shared.Infra.Http;

public class HttpLoggingHandler : DelegatingHandler
{
    // ReSharper disable once CognitiveComplexity
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var req = request;
        var id = Guid.NewGuid().ToString();
        var msg = $"[{id} -   Request]";

        Debug.WriteLine($"{msg}========Start==========");
        Debug.WriteLine($"{msg} {req.Method} {req.RequestUri?.PathAndQuery} {req.RequestUri?.Scheme}/{req.Version}");
        Debug.WriteLine($"{msg} Host: {req.RequestUri?.Scheme}://{req.RequestUri?.Host}");

        foreach (var header in req.Headers)
            Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

        if (req.Content != null)
        {
            foreach (var header in req.Content.Headers)
                Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

            if (req.Content is StringContent || IsTextBasedContentType(req.Headers) ||
                IsTextBasedContentType(req.Content.Headers))
            {
                var result = await req.Content.ReadAsStringAsync(cancellationToken);

                Debug.WriteLine($"{msg} Content:");
                Debug.WriteLine($"{msg} {string.Join("", result.Take(255))}...");
            }
        }

        var start = DateTime.Now;

        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

        var end = DateTime.Now;

        Debug.WriteLine($"{msg} Duration: {end - start}");
        Debug.WriteLine($"{msg}==========End==========");

        msg = $"[{id} - Response]";
        Debug.WriteLine($"{msg}=========Start=========");

        
        Debug.WriteLine(
            $"{msg} {req.RequestUri?.Scheme.ToUpper()}/{response.Version} {(int)response.StatusCode} {response.ReasonPhrase}");

        foreach (var header in response.Headers)
        {
            Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");
        }

        foreach (var header in response.Content.Headers)
            Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

        if (response.Content is StringContent || this.IsTextBasedContentType(response.Headers) ||
            this.IsTextBasedContentType(response.Content.Headers))
        {
            start = DateTime.Now;
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            end = DateTime.Now;

            Debug.WriteLine($"{msg} Content:");
            Debug.WriteLine($"{msg} {string.Join("", result.Take(255))}...");
            Debug.WriteLine($"{msg} Duration: {end - start}");
        }

        Debug.WriteLine($"{msg}==========End==========");
        return response;
    }

    private readonly string[] _types = new[] { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };

    bool IsTextBasedContentType(HttpHeaders headers)
    {
        if (!headers.TryGetValues("Content-Type", out var values))
            return false;
        var header = string.Join(" ", values).ToLowerInvariant();

        return _types.Any(t => header.Contains(t));
    }
}

