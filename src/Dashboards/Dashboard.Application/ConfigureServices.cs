using Authorizer.Ldap.Models;
using Dashboard.Application.Behaviors;
using Dashboard.Application.Credentials;
using Dashboard.Application.Users;
using Dashboard.Domain.Entities.LDAPs;
using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Abstractions;
using SharedKernel.Shared;
using LdapAttribute = Authorizer.Ldap.Models.LdapAttribute;

namespace Dashboard.Application;

public class ConfigureServices : IConfigureService
{
    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorResult<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblies(typeof(ConfigureServices).Assembly));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICredentialService, CredentialService>();

        ConfigureMapping();
    }

    private static void ConfigureMapping()
    {
        TypeAdapterConfig<LDAP, LdapConfiguration>
            .NewConfig()
            .MapWith(ldap => new LdapConfiguration(ldap.HostName, ldap.CredentialId, ldap.BaseDn)
            {
                Port = ldap.Port,
                UseSecure = ldap.UseSecure,
                FilterQuery = ldap.FilterQuery,
                Attributes = new LdapAttribute()
                {
                    DisplayName = ldap.Attributes.DisplayName,
                    UserName = ldap.Attributes.UserName,
                    UniqueId = ldap.Attributes.UniqueId
                },
                Scope = (System.DirectoryServices.Protocols.SearchScope)ldap.Scope,
                AuthenticationType = (System.DirectoryServices.Protocols.AuthType)ldap.AuthenticationType,
                ProtocolVersion = ldap.ProtocolVersion
            });
        
    }
}