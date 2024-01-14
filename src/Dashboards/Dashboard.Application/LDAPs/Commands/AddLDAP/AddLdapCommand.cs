using Dashboard.Domain.Entities.LDAPs.Enums;
using SharedKernel.Base.Commands;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.LDAPs.Commands.AddLDAP;

public record AddLdapCommand(
    string Name,
    int Port,
    bool UserSecure,
    string HostName,
    IdColumn CredentialId,
    string BaseDn,
    string FilterQuery = "(&(objectCategory=Person)(sAMAccountName=*))",
    SearchScope Scope = SearchScope.Subtree,
    AuthType AuthenticationType = AuthType.Negotiate,
    int ProtocolVersion = 3) : ICommand;