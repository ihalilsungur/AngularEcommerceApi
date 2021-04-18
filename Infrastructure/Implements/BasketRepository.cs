using API.Core.DbModels;
using API.Core.Interfaces;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace API.Infrastructure.Implements
{
    public  class BasketRepository:IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var created = await _database.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket),TimeSpan.FromDays(30));
            if (Equals(!created))
            {
                return null;
            }

            return await GetBasketAsync(customerBasket.Id);

        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }
    }
}
