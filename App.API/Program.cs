
using Microsoft.AspNetCore.Mvc;
using Repositories.Extensions;
using Services;
using Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<FluentValidationFilter>();
    opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; // bununla non nullable olan modeli sen kontrol etme hataları
                                                                              // referans tipli parametrelerde bunu yapıyor
                                                                              // ben kontrol ediyorum zaten diyorum true diyerek
}); // her request de çalışacak end pointtteki kendi yazdığım filterımı ekliyorum


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

var app = builder.Build();

// .Net8 ile beraber IExceptionHandler interface i geldi ve bu interface i implement ederek bu handlerlar üzerinden yakalayabiliyoruz. Yani bu
// interface ile illa da bir response dönmemize gerek yok. Hatayı ele alıp o hata yolculuğuna devam edebilir. Biz o arada mesela o hatayı ele aldık
// sonra mail veya sms gönderebiliriz sonra daha sonra detaylı bir şekilde ele alabiliriz. Global hataları ele alan handler yazıcaz bir de özel mesela
// kritik bir hata varsa da onu da başka bir handler ile ele alıcaz. Tasarımı böyle yaptık farz edelim.

app.UseExceptionHandler(x =>
{
    // bir action yazman lazım boş bırakamazsın diyor.
}); // bizim yazdığımız exceptionların çalışabilmesi için bu middleware de olması lazım, çünkü hataları yakalayan bu middleware

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

