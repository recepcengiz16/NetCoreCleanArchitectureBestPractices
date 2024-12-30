using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repositories;

namespace Services.Filters;

public class NotFoundFilter<T,TId>(IGenericRepository<T,TId> repository): Attribute,IAsyncActionFilter where T : class where TId : struct
// burada attribute da ekledik global olarak değil de bazı actionlarda kullanmak istiyorum diye
{
   
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // action metod çalışmadan await next(); den önce yazıyoruz. 
        
        // ActionArguments controllerdaki endpointlerin aldığı parametrelerdir.
        var idValue = context.ActionArguments.TryGetValue("id",out var idAsObject) ? idAsObject : null; // parametre ismi olarak id olanı aldık.
                                                                                                        //  Çünkü kontrol ederken veritabanında hepsinde
                                                                                                        // bu property(Id) olduğu için
                                                                                                        
        if (idValue is not TId id)
        {
            await next(); // endpointi çalıştır ve
            return; // bu metottaki alt taraftaki işlemleri yapmasın diye return yazdık
        }

        var anyEntity = await repository.AnyAsync(id);
        if (anyEntity)
        {
            await next();
            return;
        }
        
        var entityName = typeof(T).Name;
        var actionMethodName = context.ActionDescriptor.RouteValues["action"];

        var result = ServiceResult.Fail($"Data bulunamamıştır. {entityName} - {actionMethodName}");
        context.Result = new NotFoundObjectResult(result);
        
       
        // action metod çalıştıktan sonra yazmak istiyorsak await next(); sonra buraya yazıyoruz. 
    }
}