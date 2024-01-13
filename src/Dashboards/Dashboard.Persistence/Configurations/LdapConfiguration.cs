using Dashboard.Domain.Entities.LDAPs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dashboard.Persistence.Configurations;

public class LdapConfiguration : IEntityTypeConfiguration<LDAP>
{
    public void Configure(EntityTypeBuilder<LDAP> builder)
    {
        builder.ToTable("LDAPs");
        builder.HasKey(u => u.Id);

        builder.HasIndex(l => l.Name)
            .IsUnique();

        builder.Property(l => l.Port)
            .IsRequired();

        builder.Property(l => l.UseSecure)
            .IsRequired();

        builder.Property(l => l.HostName)
            .IsRequired();

        builder.Property(l => l.CredentialId)
            .IsRequired();

        builder.Property(l => l.BaseDn)
            .IsRequired();

        builder.Property(l => l.FilterQuery)
            .IsRequired();

        builder.Property(l => l.Scope)
            .IsRequired();

        builder.Property(l => l.AuthenticationType)
            .IsRequired();

        builder.Property(l => l.ProtocolVersion)
            .IsRequired();

        builder.ComplexProperty(l => l.Attributes)
            .IsRequired();

    }
}