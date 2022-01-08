using Basket.Api.Entities;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private const string _cacheNameSpace = "shoppingcart";

        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer multiplexer)
        {
            _database = multiplexer.GetDatabase();
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            RedisValue[] items = await _database.ListRangeAsync(GetRedisKey(userName), 0, -1);
            if (items.Length == 0)
                return new ShoppingCart(userName);
            else
            {
                ShoppingCart cart = new ShoppingCart(userName);
                foreach(RedisValue item in items)
                {
                    ShoppingCartItem itemToAdd = JsonSerializer.Deserialize<ShoppingCartItem>(item.ToString());
                    cart.Items.Add(itemToAdd);
                }
                return cart;
            }
        }

        public async Task<long> UpdateShoppingCartItem(string userName, ShoppingCartItem item)
        {
            string itemToAdd = JsonSerializer.Serialize(item);
            long count = await _database.ListRightPushAsync(GetRedisKey(userName), new RedisValue[]
            {
                new RedisValue(itemToAdd)
            });
            return count;
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            bool status = await _database.KeyDeleteAsync(GetRedisKey(userName));
            return status;
        }

        Func<string, RedisKey> GetRedisKey = (key) =>
        {
            string keyToUse = $"{_cacheNameSpace}:{key}";
            return new RedisKey(keyToUse);
        };
    }
}
