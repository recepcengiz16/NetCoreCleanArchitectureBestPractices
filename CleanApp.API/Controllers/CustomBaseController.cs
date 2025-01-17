using System.Net;
using App.Application;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResult<T>(ServiceResult<T> result)
        {
            if (result.Status == HttpStatusCode.NoContent)
            {
               return NoContent();
            }

            if (result.Status == HttpStatusCode.Created)
            {
                return Created(result.UrlAsCreated, result); // burada created dönünce headerına yazıyor url yi, bodysine de result ı yazıyor.
            }
            
            return new ObjectResult(result)
            {
                StatusCode = (int)result.Status,
            };
        }
        
        [NonAction] // end point olmadığınu belirtmek için, yardımcı olsunlar diye yazdık ya bu metotları
        public IActionResult CreateActionResult(ServiceResult result)
        {
            if (result.Status == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) // eğer boş ise no content ve null olarak döndük datayı. Ok, BadRequest,  unauthorized vs
                {
                    StatusCode = (int)result.Status,
                };
            }
            
            return new ObjectResult(result)
            {
                StatusCode = (int)result.Status,
            };
        }
    }
}
