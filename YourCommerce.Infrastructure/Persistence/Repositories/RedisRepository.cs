using YourCommerce.Domain.Redis;
using StackExchange.Redis;
using System.Text.Json;

namespace YourCommerce.Infrastructure.Persistence.Repository;

public class RedisRepository : IRedisRepository
{
    private readonly IConnectionMultiplexer _redis;

    public RedisRepository(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<T> GetCacheAsync<T>(string key)
    {
        var redisDb = _redis.GetDatabase();
        
        var json = await redisDb.StringGetAsync(key);

        if(json.HasValue)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        return default(T);
    }

    public async Task SaveCacheAsync<T>(string key, T value)
    {
         var db = _redis.GetDatabase();
        var json = JsonSerializer.Serialize(value);
        await db.StringSetAsync(key, json);
    }
}