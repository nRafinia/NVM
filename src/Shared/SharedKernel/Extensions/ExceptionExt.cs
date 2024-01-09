using System.Data;

namespace SharedKernel.Extensions;

public static class ExceptionExt
{
    public static Result ToResult(this Exception exception)
    {
        return exception switch
        {
            ArgumentNullException => Result.Failure(SharedErrors.InvalidArguments),
            DuplicateNameException => Result.Failure(SharedErrors.Duplicate(exception.Message)),
            _ => Result.Failure(SharedErrors.InternalErrorMessage(exception.Message))
        };
    }
    
    public static Result<T?> ToResult<T>(this Exception exception)
    {
        return Result.Failure<T>(ToResult(exception).Error!);
    }
}