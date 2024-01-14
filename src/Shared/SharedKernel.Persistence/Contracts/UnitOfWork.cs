using MediatR;
using SharedKernel.Abstractions;
using SharedKernel.Persistence.Base;

namespace SharedKernel.Persistence.Contracts;

public class UnitOfWork(
    ApplicationDbContext context,
    IPublisher publisher,
    IDateTime dateTime,
    ICurrentUser user)
    : BaseUnitOfWork<ApplicationDbContext>(context, publisher, dateTime, user), IUnitOfWork
{
}