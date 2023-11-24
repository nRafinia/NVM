namespace Agent.UI.Helpers;

public interface IJavaScriptLoader
{
    ValueTask Load(string jsPath);
}