using Microsoft.AspNetCore.Http;
using Shared.Domain.Base;
using Shared.Domain.Base.Results;
using Shared.Domain.Errors;

namespace Shared.Presentation.HttpEndpointHandlers;

public static class HttpResponseResultExt
{
    public static IResult GetHttpResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok(result.Value!);
        }

        var error = result.Error!;
        return HandleErrors(error);
    }

    public static IResult GetHttpResponse(this Result result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok();
        }

        var error = result.Error!;
        return HandleErrors(error);
    }

    private static IResult HandleErrors(Error error)
    {
        return error.Code switch
        {
            SharedErrors.InvalidArgumentsCode => ValidationException(error),
            SharedErrors.ItemNotFoundCode => NotFoundException(error),
            AuthenticateErrors.AuthenticationFailedName or
            AuthenticateErrors.AccountIsNotActiveName or
            AuthenticateErrors.TokenIsExpiredName => AuthenticationFailedException(error),
            _ => Results.Problem(error.Message, title: error.Code)
        };
    }

    private static IResult ValidationException(Error error)
    {
        var errors = error.GetData<Dictionary<string, string[]>>();

        if (errors is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>(0),
                detail: error.Code,
                title: error.Message);
        }

        var result = Results.ValidationProblem(errors, detail: error.Message, title: error.Code);
        return result;
    }

    private static IResult NotFoundException(Error error)
    {
        return Results.NotFound(error.Message);
    }

    private static IResult AuthenticationFailedException(Error error)
    {
        return Results.Content(error.Message, statusCode: 401);
    }
}