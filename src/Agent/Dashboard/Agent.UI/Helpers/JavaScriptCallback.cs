using Microsoft.JSInterop;

namespace Agent.UI.Helpers;

public class JavaScriptCallback<T> : IDisposable
{
    private Func<T, Task>? _action;
    private readonly IJSRuntime _jsRuntime;
    private DotNetObjectReference<JavaScriptCallback<T>>? _objRef;

    public JavaScriptCallback(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public ValueTask Set(Func<T, Task> action)
    {
        _action = action;
        _objRef = DotNetObjectReference.Create(this);
        return _jsRuntime.InvokeVoidAsync("setCallback", _objRef);
    }

    [JSInvokable]
    public Task Call(T args)
    {
        return _action?.Invoke(args) ?? Task.CompletedTask;
    }

    public void Dispose()
    {
        _objRef?.Dispose();
    }

    ~JavaScriptCallback()
    {
        Dispose();
    }
}