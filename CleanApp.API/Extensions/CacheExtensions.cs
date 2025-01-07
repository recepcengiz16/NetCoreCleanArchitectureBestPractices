using App.Application.Contracts.Caching;
using App.Caching;

namespace CleanApp.API.Extensions;

public static class CacheExtensions
{
    public static IServiceCollection AddCacheExtension(this IServiceCollection services)
    {
        
        services.AddMemoryCache();
        services.AddMemoryCache(); // inmemory cache kullandığımız için ekledik bunu da.
        services.AddSingleton<ICacheService, CacheService>();
        return services;
    }
}