using System.Reflection;
using SharedKernel.Persistence.Abstractions;

namespace Dashboard.Services;

public class ProjectAssetsService(List<Assembly> assemblies) : IProjectAssets
{
    public IReadOnlyList<Assembly> Assemblies { get; } = assemblies;
}