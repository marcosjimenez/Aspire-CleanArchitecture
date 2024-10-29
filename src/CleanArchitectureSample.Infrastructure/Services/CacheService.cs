using CleanArchitectureSample.Application.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace CleanArchitectureSample.Infrastructure.Services
{
    public class CacheService(IDistributedCache cache) : ICacheService
    {
        private readonly IDistributedCache _cache = cache ??
            throw new ArgumentNullException(nameof(cache));

        public async Task<T?> GetAsync<T>(string cacheName)
        {
            var item = await _cache.GetAsync(cacheName);
            return item is null ?
                default:
                JsonSerializer.Deserialize<T>(item) ?? default!;
        }

        public async Task RemoveAsync(string cacheName)
            => await _cache.RemoveAsync(cacheName);

        public async Task SetAsync<T>(string cacheName, T item)
            => await SetAsync(cacheName, item, null, null);

        public async Task SetAsync<T>(string cacheName, T item, TimeSpan? expirationFromNow = null)
            => await SetAsync(cacheName, item, expirationFromNow, null);

        public async Task SetAsync<T>(string cacheName, T item, TimeSpan? expirationFromNow, TimeSpan? slidingExpiration)
            => await _cache.SetAsync(cacheName, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(item)),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expirationFromNow ?? null,
                    SlidingExpiration = slidingExpiration ?? DateTime.Now.AddMinutes(10).TimeOfDay,
                });
    }
}
