using Dashboard.Domain.Entities.LDAPs;
using Dashboard.Domain.Entities.LDAPs.Enums;
using SharedKernel.Base.Commands;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.LDAPs.Commands.UpdateLDAP;

public record UpdateLdapCommand(
    IdColumn Id,     
    string Name,
    int Port,
    bool UserSecure,
    string HostName,
    IdColumn CredentialId,
    string BaseDn,
    string FilterQuery,
    SearchScope Scope,
    AuthType AuthenticationType,
    int ProtocolVersion) : ICommand<LDAP>;