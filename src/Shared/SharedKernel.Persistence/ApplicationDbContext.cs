using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence.Abstractions;

namespace SharedKernel.Persistence;

public class ApplicationDbContext(DbContextOptions options, IProjectAssets projectAssets)
    : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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

}