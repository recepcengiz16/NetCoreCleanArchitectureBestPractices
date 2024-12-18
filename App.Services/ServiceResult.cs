using System.Net;
using System.Text.Json.Serialization;

namespace Services;

// hata veya başarılı olduğunda gözükecek olan model
public class ServiceResult<T>
{
    public T? Data { get; set; }
    public List<string> ErrorMessages { get; set; }
    
    [JsonIgnore] // bunlar response ta gözükmesin diye
    public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;
    
    [JsonIgnore]
    public bool IsFail => !IsSuccess;
    
    [JsonIgnore]
    public HttpStatusCode Status { get; set; }

    [JsonIgnore]
    public string? UrlAsCreated { get; set; } // response da created olduğunda o yeni oluşan dataya nasıl erişebileceğimi gösteren url

    // static factory method olarak geçiyor ve newlemeyi kontrol altına almış oluyoruz bu şekilde
    public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            Status = status,
        };
    }
    
    public static ServiceResult<T> SuccessAsCreated(T data, string url) // niye sadece burada yazdık alttaki success durumu başarılı doluğunda
                                                                        // bir şey dönmeyen biz de başarılı olunca bir şey dönsün istiyoruz.
    {
        return new ServiceResult<T>()
        {
            Data = data,
            Status = HttpStatusCode.Created,
            UrlAsCreated = url,
        };
    }

    public static ServiceResult<T> Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessages = errorMessages,
            Status = status,
        };
    }

    public static ServiceResult<T> Fail(string message, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessages = [message],
            Status = status,
        };
    }
} 

// update gibi işlemlerde sadece başarılı olduğunda dönecek model. T yok farkındaysan
public class ServiceResult
{
    public List<string> ErrorMessages { get; set; }
    
    [JsonIgnore]
    public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;
    
    [JsonIgnore]
    public bool IsFail => !IsSuccess;
    
    [JsonIgnore]
    public HttpStatusCode Status { get; set; }

    // static factory method olarak geçiyor ve newlemeyi kontrol altına almış oluyoruz bu şekilde
    public static ServiceResult Success( HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult()
        {
            Status = status,
        };
    }

    public static ServiceResult Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMessages = errorMessages,
            Status = status,
        };
    }

    public static ServiceResult Fail(string message, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMessages = [message],
            Status = status,
        };
    }
}