using AutoMapper;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : Protos.DiscountService.DiscountServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;
        public DiscountService(IDiscountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<Coupon> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            Entities.Coupon coupon = await _repository.GetDiscount(request.ProductName);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} not found"));
            Coupon couponModel = _mapper.Map<Coupon>(coupon);
            return couponModel;
        }

        public override async Task<Coupon> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            Entities.Coupon coupon = _mapper.Map<Entities.Coupon>(request.Coupon);
            await _repository.CreateDiscount(coupon);
            Coupon couponModel = _mapper.Map<Coupon>(coupon);
            return couponModel;
        }

        public override async Task<Coupon> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            Entities.Coupon coupon = _mapper.Map<Entities.Coupon>(request.Coupon);
            await _repository.UpdateDiscount(coupon);
            Coupon couponModel = _mapper.Map<Coupon>(coupon);
            return couponModel;

        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            bool isRecordDeleted = await _repository.DeleteDiscount(request.ProductName);
            DeleteDiscountResponse response = new DeleteDiscountResponse
            {
                Success = isRecordDeleted
            };
            return response;
        }
    }
}
