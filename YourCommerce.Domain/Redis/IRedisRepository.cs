namespace YourCommerce.Domain.Redis;

public interface IRedisRepository
{
    Task SaveCacheAsync<T>(string key, T value);
    Task<T> GetCacheAsync<T>(string key);
}