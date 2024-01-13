using Dashboard.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dashboard.Persistence.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(l => l.UserName)
            .IsRequired();
        builder.Property(l => l.Password)
            .IsRequired(false);
        builder.Property(l => l.DisplayName)
            .IsRequired();
        builder.Property(l => l.Status)
            .IsRequired();
        builder.Property(l => l.AuthorizerType)
            .IsRequired();
        builder.Property(l => l.Status)
            .IsRequired();

        builder.Property(u => u.Created).IsRequired();
        builder.Property(u => u.CreatedBy);
        builder.Property(u => u.LastUpdated);
        builder.Property(u => u.LastUpdatedBy);
        
        /*builder.HasOne(u => u.Ldap)
            .WithMany(u => u.Users)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);*/
    }
}