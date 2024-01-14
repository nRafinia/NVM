using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence.Abstractions;
using SharedKernel.Persistence.Converters;
using SharedKernel.ValueObjects;

namespace SharedKernel.Persistence;

public class ApplicationDbContext(DbContextOptions options, IProjectAssets projectAssets)
    : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Database.Migrate();
        
        foreach (var assembly in projectAssets.Assemblies)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<IdColumn>()
            .HaveConversion<IdColumnConverter>();
    }
}