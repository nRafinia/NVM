namespace SharedKernel.Abstractions;

public interface ICurrentUser
{
    IdColumn GetUserId();
}