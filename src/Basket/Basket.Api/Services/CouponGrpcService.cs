using Basket.Api.Entities;
using Discount.Grpc.Protos;
using System.Threading.Tasks;
using static Discount.Grpc.Protos.DiscountService;

namespace Basket.Api.Services
{
    public class CouponGrpcService : ICouponGrpcService
    {
        private DiscountServiceClient _client;
        public CouponGrpcService(DiscountServiceClient client)
        {
            _client = client;
        }

        public async Task<ShoppingCartItem> GetPrice(ShoppingCartItem item)
        {
            GetDiscountRequest request = new GetDiscountRequest
            {
                ProductName = item.ProductName
            };
            Coupon coupon = await _client.GetDiscountAsync(request);
            item.Price -= coupon.Amount;
            return item;
        }
    }
}
