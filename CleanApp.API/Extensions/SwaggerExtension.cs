namespace CleanApp.API.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1",new (){ Title = "CleanApp.API", Version = "v1" });
        });
        return services;
    }

    public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder app)
    {
        app.UseSwagger(); // bunlar da app.use lu fonksiyonlar vardı ya orada kullandığımız swagger fonksiyonları
        app.UseSwaggerUI();
        return app;
    }
}