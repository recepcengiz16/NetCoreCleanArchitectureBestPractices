using App.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CleanApp.API.ExceptionHandler;

public class CriticalExceptionHandler(ILogger<CriticalExceptionHandler> logger): IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is CriticalException)
        {
            Console.WriteLine("Hata ile ilgili sms gönderildi");
        }
        return ValueTask.FromResult(false);
    }
}