using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Services.ExceptionHandler;

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