using System.Reflection;

namespace SharedKernel.Persistence.Abstractions;

public interface IProjectAssets
{
    public IReadOnlyList<Assembly> Assemblies { get; }
}