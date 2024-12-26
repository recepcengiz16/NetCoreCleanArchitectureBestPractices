using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Services;

public class FluentValidationFilter: IAsyncActionFilter // kendi filterımızı yazıyoruz neden async yüksek trafikli uygulamalarda hızlı cevaplama olsun diye
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.// burada modelstate üzerinden hatalara bakıyorum
                Values.
                SelectMany(x => x.Errors).
                Select(x => x.ErrorMessage)
                .ToList();
            
            var resultModel = ServiceResult.Fail(errors);
            context.Result = new BadRequestObjectResult(resultModel); // burada da 400 ile yani badrequest ile context.result u yani geri dönüşünü
                                                                      // ayarladık. Kullanıcının yaptığı hataydı o yüzden 400 ile döndük. Server
                                                                      // tarafında hata varsa 500 durum kodunu kullanıcaz
            return; // hatalar olduğu için next ile end pointe gitmesin
        }
        await next(); // hiçbir hata olmadığında çalışacak metod
    }
}