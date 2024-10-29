namespace CleanArchitectureSample.Application.Contracts
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string cacheName); 
        Task RemoveAsync(string cacheName);
        Task SetAsync<T>(string cacheName, T item);
        Task SetAsync<T>(string cacheName, T item, TimeSpan? expirationFromNow = null);
        Task SetAsync<T>(string cacheName, T item, TimeSpan? expirationFromNow = null, TimeSpan? slidingExpiration = null);
    }
}

