using CleanApp.API.Filters;

namespace CleanApp.API.Extensions;

public static class ControllerExtensions
{
    public static IServiceCollection AddControllersWithFiltersExt(this IServiceCollection services)
    {
        services.AddScoped(typeof(NotFoundFilter<,>)); // iki tane virgül aldığı için bu şekilde yazdık.
        
        services.AddControllers(opt =>
        {
            opt.Filters.Add<FluentValidationFilter>();
            opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; // bununla non nullable olan modeli sen kontrol etme hataları
            // referans tipli parametrelerde bunu yapıyor
            // ben kontrol ediyorum zaten diyorum true diyerek
        }); // her request de çalışacak end pointtteki kendi yazdığım filterımı ekliyorum

        
        return services;
    }
}