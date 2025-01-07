using CleanApp.API.ExceptionHandler;

namespace CleanApp.API.Extensions;

public static class ExceptionHandlerExtension
{
    public static IServiceCollection AddExceptionHandlerExtension(this IServiceCollection services)
    {
        services.AddExceptionHandler<CriticalExceptionHandler>(); // eklediğim sıra önemli çünkü critical da false döndüğümüz için diğer exceptionhandlera girecek
        services.AddExceptionHandler<GlobalExceptionHandler>(); // buradaki sınıfta da return true döndüğümüz için başka exceptionhandlera girmeyecek
        return services;
    }
}