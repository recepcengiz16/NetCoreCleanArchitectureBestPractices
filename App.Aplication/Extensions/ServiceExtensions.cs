using System.Reflection;
using App.Application.Features.Categories;
using App.Application.Features.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<ApiBehaviorOptions>(opt=>opt.SuppressModelStateInvalidFilter=true); // .net in ürettiği default hata mesajını
        // kapattık ki bizim ürettiğimiz sonuç
        // hatalı da olsa doğru da olsa o modeli dönsün 
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddFluentValidationAutoValidation(); // fluent validationı otomatik olarak tanı
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // yazacağımız validation classlarını tanıması için 
        services.AddAutoMapper(Assembly.GetExecutingAssembly()); // automapperı da ekledik
       
        return services;
    }
}