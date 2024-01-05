using System.Reflection;

namespace Dashboard.Application.Extensions;

internal static class ObjectExt
{
    public static bool AllPropertiesHaveValue(this object obj)
    {
        return obj.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.GetProperty)
            .All(p => !IsDefault(p.GetValue(obj)));
    }
    
    public static bool OneOfPropertiesMustHaveValue(this object obj)
    {
        return obj.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.GetProperty)
            .Any(p => !IsDefault(p.GetValue(obj)));
    }

    private static bool IsDefault<T>(this T value)
    {
        return EqualityComparer<T>.Default.Equals(value, default);
    }
}