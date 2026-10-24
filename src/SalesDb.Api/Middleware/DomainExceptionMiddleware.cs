using SalesDb.Application.Common;
using SalesDb.Domain.Exceptions;

namespace SalesDb.Api.Middleware;


public sealed class DomainExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DomainException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(ApiResponse<object>.Fail($"[DOMAIN] - {ex.Message}"));
        }
    }
}

