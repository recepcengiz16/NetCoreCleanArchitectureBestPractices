using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Services.ExceptionHandler;

public class GlobalExceptionHandler: IExceptionHandler // Özel bir hata olunca sms veya mail göndermek için oluşturduk.
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        // return true dönersem bu hatay ben ele alıcam demek. Eğer false dönersem de bu hatayı bir sonraki handlera aktarmak istiyorum ya da
        // ilgili middleware a gitsin demek. Biz globalexceptionhandler da hatayı ele almak istiyorum bu yüzden
        // return true diyerek response u mu dönücem
        var errorAsDto = ServiceResult.Fail(exception.Message, HttpStatusCode.InternalServerError); // InternalServerError : 500 hatası 
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";
        
        await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken);

        return true; // true dönerek bu hatayı kendim ele aldım demiş olduk. Bundan sonra kimse ele almasın hata olaylarını bizim burada yazdığımız
        // dto yu dönecek
    }
}