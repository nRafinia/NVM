using Microsoft.JSInterop;

namespace Agent.UI.Helpers;

public class JavaScriptLoader : IJavaScriptLoader
{
    private readonly IJSRuntime _jsRuntime;

    public JavaScriptLoader(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public ValueTask Load(string jsPath)
    {
        return _jsRuntime.InvokeVoidAsync("loadScript", jsPath);
    }
}