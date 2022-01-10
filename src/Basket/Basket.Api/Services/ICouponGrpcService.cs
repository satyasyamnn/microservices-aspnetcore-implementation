using Basket.Api.Entities;
using System.Threading.Tasks;

namespace Basket.Api.Services
{
    public interface ICouponGrpcService
    {
        Task<ShoppingCartItem> GetPrice(ShoppingCartItem item);
    }
}
