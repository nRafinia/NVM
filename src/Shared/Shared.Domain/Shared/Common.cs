using System.Reflection;

namespace Shared.Domain.Shared;

public static class Common
{
    public static IEnumerable<T?> GetImplementedInterfaceOf<T>(Assembly assembly)
        where T : class
    {
        return GetImplementedInterfaceOf(typeof(T), assembly).ToList()
            .Select(t => t as T);
    }

    public static IEnumerable<Type> GetImplementedInterfaceOf(Type type, Assembly assembly)
    {
        return assembly
            .GetExportedTypes()
            .Where(t => type.IsAssignableFrom(t) && !t.IsInterface)
            .GroupBy(a => a)
            .Select(a => a.Key);
    }
}