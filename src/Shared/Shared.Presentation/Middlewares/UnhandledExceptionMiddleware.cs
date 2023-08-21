using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Shared.Presentation.Middlewares;

public class UnhandledExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public UnhandledExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            await ValidationException(exception, context);
        }
        catch (UnauthorizedAccessException)
        {
            await UnauthorizedAccessException(context);
        }
        catch (Exception exception)
        {
            await UnknownException(exception, context);
        }
    }

    private static Task ValidationException(ValidationException exception, HttpContext context)
    {
        var errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(e => e.Key, e => e.Select(m => m.ErrorMessage).ToArray());
        var result = Results.ValidationProblem(errors, title: exception.Message);
        return result.ExecuteAsync(context);
    }

    private static Task UnknownException(Exception exception, HttpContext context)
    {
        var result = Results.Problem(title: exception.Message);
        return result.ExecuteAsync(context);
    }

    private static Task UnauthorizedAccessException(HttpContext context)
    {
        var result = Results.Unauthorized();
        return result.ExecuteAsync(context);
    }
}