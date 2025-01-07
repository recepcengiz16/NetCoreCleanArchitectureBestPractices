namespace CleanApp.API.Extensions;

public static class ConfigurePipelineExtension
{
    public static IApplicationBuilder UseConfigurePipelineExtension(this WebApplication app)
    {
        app.UseExceptionHandler(x =>
        {
            // bir action yazman lazım boş bırakamazsın diyor.
        }); // bizim yazdığımız exceptionların çalışabilmesi için bu middleware de olması lazım, çünkü hataları yakalayan bu middleware


// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerExtension(); // kendi yazdığımız extensionlar
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        return app;
    }
}