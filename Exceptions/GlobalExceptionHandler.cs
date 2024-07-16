using clerk.server.Helpers;
using Microsoft.AspNetCore.Diagnostics;

namespace clerk.server.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        await httpContext.Response.WriteAsJsonAsync(
            Result.Exception(exception.Message)
        );
        return true;
    }
}