using System.Reflection;

namespace SharedKernel.Shared;

public static class Common
{
    public static IEnumerable<T?> GetImplementedInterfaceOf<T>(params Assembly[] assemblies)
        where T : class
    {
        return GetImplementedInterfaceOf(typeof(T), assemblies)
            .ToList()
            .Select(t => t as T);
    }

    public static IEnumerable<object?> GetImplementedInterfaceOf(Type type, params Assembly[] assemblies)
    {
        return assemblies
            .Select(a => a.GetExportedTypes())
            .SelectMany(t => t)
            .Where(t => type.IsAssignableFrom(t) && !t.IsInterface)
            .GroupBy(a => a)
            .Select(a => Activator.CreateInstance(a.Key));
    }
}