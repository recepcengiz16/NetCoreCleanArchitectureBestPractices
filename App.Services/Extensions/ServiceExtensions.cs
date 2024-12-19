using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Products;

namespace Services.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddFluentValidationAutoValidation(); // fluent validationı otomatik olarak tanı
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // yazacağımız validation classlarını tanıması için 
        return services;
    }
}