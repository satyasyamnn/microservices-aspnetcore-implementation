using Basket.Api.Entities;
using System.Threading.Tasks;

namespace Basket.Api.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<long> UpdateShoppingCartItem(string userName, ShoppingCartItem item);
        Task<bool> DeleteBasket(string userName);
    }
}
