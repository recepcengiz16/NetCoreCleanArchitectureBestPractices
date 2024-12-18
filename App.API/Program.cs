
using Microsoft.AspNetCore.Mvc;
using Repositories.Extensions;
using Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(opt=>opt.SuppressModelStateInvalidFilter=true); // .net in ürettiği default hata mesajını
                                                                                               // kapattık ki bizim ürettiğimiz sonuç
                                                                                               // hatalı da olsa doğru da olsa o modeli dönsün 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

var app = builder.Build();

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

