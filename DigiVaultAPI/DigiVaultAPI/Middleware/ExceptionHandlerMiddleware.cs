using DigiVaultAPI.Exceptions;
using FluentValidation;
using System.Text.Json;

namespace DigiVaultAPI.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static async Task HandleException(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, body) = ex switch
        {
            NotFoundException e      => (404, new { message = e.Message }),
            UnauthorizedException e  => (401, new { message = e.Message }),
            ForbiddenException e     => (403, new { message = e.Message }),
            ConflictException e      => (409, new { message = e.Message }),
            ValidationException e    => (400, new { errors = e.Errors.Select(x => x.ErrorMessage).ToList() } as object),
            _                        => (500, new { message = "Wystąpił nieoczekiwany błąd." } as object)
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(body));
    }
}