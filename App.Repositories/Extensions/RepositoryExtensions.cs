using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            // AddDbContext i repository katmanında kullanabilmem için (çünkü repostory katmanı bir class library) App.Repositories.csproj kısmını library üzerine gelip
            // edit ile açıp frameworkreference ı eklemem lazım
    
            var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>(); 
            // burada direk yazmamaız lazım ya GetSection("ConnectionStrings") diye 
            // options pattern connectionStringi direk IConfgiguration ile değil de tip güvenlli yaklaşım ile okumamızı sağlıyor. Onu da repository katmanında tanımlamamız lazım.

            options.UseNpgsql(connectionString!.PostgreSql, opt =>
            {
                opt.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName); // migrationlar bu katmanda olsun diye. Normalde dbcontext yazmak yerine çünkü yarın öbür gün
                                                                                            // birden fazla context olabilir, adı değişebilir vs.
                                                                                            // bir tane class oluştrup bu dizinde onu verdik.
            }); // connectionString! bu da Bu atamanın güvenli olduğunu garanti ediyorum compilera,  buraya bir şey gelecek manasında asında.
            // Compilerı rahatlatıyoruz aslında
        });
        return services; // hani . deyip başka metotları da ekliyorduk ya zincir şekilnde o yüzden IServiceCollection döndük.
    }
}