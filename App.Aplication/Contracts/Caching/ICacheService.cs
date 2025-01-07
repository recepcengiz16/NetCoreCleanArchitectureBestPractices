namespace App.Application.Contracts.Caching;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    Task AddAsync<T>(string key, T value, TimeSpan? expiryTimeSpan); // expiryTimeSpan ne kadar cache de kalacak.
    Task RemoveAsync(string key);
}