using CleanArchitectureSample.Application.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text;

namespace CleanArchitectureSample.Infrastructure
{
    public class CacheManager (IDistributedCache cache) : ICacheManager
    {
        private readonly IDistributedCache _cache = cache ??
            throw new ArgumentNullException(nameof(cache));

        public async Task<T> GetAsync<T>(string cacheName)
        {
            var item = await _cache.GetAsync(cacheName);
            return item is null ?
                default!:
                JsonSerializer.Deserialize<T>(item) ?? default!;
        }

        public async Task Set<T>(string cacheName, T item)
            => await _cache.SetAsync(cacheName, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(item)),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = DateTime.Now.AddSeconds(10).TimeOfDay
                });
    }
}
