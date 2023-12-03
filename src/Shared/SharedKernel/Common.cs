using System.Reflection;

namespace SharedKernel;

public static class Common
{
    public static IEnumerable<T?> GetImplementedInterfaceOf<T>(params Assembly[] assemblies)
        where T : class
    {
        return GetImplementedInterfaceOf(typeof(T), assemblies)
            .ToList()
            .Select(t => t as T);
    }

    public static IEnumerable<Type> GetImplementedInterfaceOf(Type type, params Assembly[] assemblies)
    {
        return assemblies
            .Select(a => a.GetExportedTypes())
            .SelectMany(t => t)
            .Where(t => type.IsAssignableFrom(t) && !t.IsInterface)
            .GroupBy(a => a)
            .Select(a => a.Key);
    }
}