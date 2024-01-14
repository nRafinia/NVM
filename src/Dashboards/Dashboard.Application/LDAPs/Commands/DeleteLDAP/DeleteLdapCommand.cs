using SharedKernel.Base.Commands;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.LDAPs.Commands.DeleteLDAP;

public record DeleteLdapCommand(IdColumn Id) : ICommand;