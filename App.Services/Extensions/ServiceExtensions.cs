using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Categories;
using Services.ExceptionHandler;
using Services.Filters;
using Services.Products.Create;

namespace Services.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<ApiBehaviorOptions>(opt=>opt.SuppressModelStateInvalidFilter=true); // .net in ürettiği default hata mesajını
        // kapattık ki bizim ürettiğimiz sonuç
        // hatalı da olsa doğru da olsa o modeli dönsün 
        services.AddScoped(typeof(NotFoundFilter<,>)); // iki tane virgül aldığı için bu şekilde yazdık.
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddFluentValidationAutoValidation(); // fluent validationı otomatik olarak tanı
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // yazacağımız validation classlarını tanıması için 
        services.AddAutoMapper(Assembly.GetExecutingAssembly()); // automapperı da ekledik

        services.AddExceptionHandler<CriticalExceptionHandler>(); // eklediğim sıra önemli çünkü critical da false döndüğümüz için diğer exceptionhandlera girecek
        services.AddExceptionHandler<GlobalExceptionHandler>(); // buradaki sınıfta da return true döndüğümüz için başka exceptionhandlera girmeyecek
        return services;
    }
}