using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.ExceptionHandler;
using Services.Products.Create;

namespace Services.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddFluentValidationAutoValidation(); // fluent validationı otomatik olarak tanı
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // yazacağımız validation classlarını tanıması için 
        services.AddAutoMapper(Assembly.GetExecutingAssembly()); // automapperı da ekledik

        services.AddExceptionHandler<CriticalExceptionHandler>(); // eklediğim sıra önemli çünkü critical da false döndüğümüz için diğer exceptionhandlera girecek
        services.AddExceptionHandler<GlobalExceptionHandler>(); // buradaki sınıfta da return true döndüğümüz için başka exceptionhandlera girmeyecek
        return services;
    }
}