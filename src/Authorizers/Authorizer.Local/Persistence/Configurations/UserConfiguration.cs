using Authorizer.Local.Domain;
using Authorizer.Local.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorizer.Local.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.UserName).IsUnique();

        builder.Property(u => u.Id)
            .IsRequired();

        builder.Property(u => u.UserName)
            .IsRequired();

        builder.Property(u => u.Password)
            .IsRequired();

        builder.Property(u => u.DisplayName)
            .IsRequired();
        
        builder.Property(u => u.Status)
            .IsRequired();

        builder.Property(u => u.Created);
    }
}