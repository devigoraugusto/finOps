using finOps.Application.Interfaces.Repositories;
using finOps.Core.Cache;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;

namespace finOps.Infra.Cache.Repositories
{
    public class CartCacheRepository : ICartCacheRepository
    {
        private readonly IDatabase _database;
        public CartCacheRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public IConfiguration _configuration;
        private static string GetCartCacheKey(Guid companyGuid) => $"CartCache:{companyGuid}";

        public async Task<CartCache?> GetCartCacheAsync(Guid companyGuid)
        {
            var value = await _database.StringGetAsync(GetCartCacheKey(companyGuid));
            if (value.IsNullOrEmpty) return null;

            return JsonSerializer.Deserialize<CartCache>(value!);
        }

        public async Task SaveCartCache(CartCache cartCache)
        {
            var GetCartCacheExpirationInMinutes = double.Parse(_configuration["CacheRedis:TimeCache"]);

            var key = GetCartCacheKey(cartCache.CompanyGuid);
            var value = JsonSerializer.Serialize(cartCache);
            await _database.StringSetAsync(key, value, TimeSpan.FromMinutes(GetCartCacheExpirationInMinutes));
        }

        public async Task DeleteCartCache(Guid companyGuid)
        {
            var key = GetCartCacheKey(companyGuid);
            await _database.KeyDeleteAsync(key);
        }
    }
}
