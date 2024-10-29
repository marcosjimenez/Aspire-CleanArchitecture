namespace CleanArchitectureSample.Application.Contracts
{
    public interface ICacheManager
    {
        Task<T> GetAsync<T>(string cacheName);
        Task Set<T>(string cacheName, T item);
    }
}

