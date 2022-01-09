using Dapper;
using Discount.Grpc.Configuration;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DiscountDataBaseSettings _configuration;
        public DiscountRepository(IOptions<DiscountDataBaseSettings> configuration)
        {
            _configuration = configuration.Value;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.ConnectionString))
            {
                var affected = await connection.ExecuteAsync("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                    new { coupon.ProductName, coupon.Description, coupon.Amount });
                if (affected == 0)
                    return false;
                return true;
            }
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.ConnectionString))
            {
                var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                    new { ProductName = productName });
                if (affected == 0)
                    return false;
                return true;
            }
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.ConnectionString))
            {
                Coupon coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });
                if (coupon == null)
                    return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
                return coupon;
            }
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.ConnectionString))
            {
                var affected = await connection.ExecuteAsync("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                                new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id });
                if (affected == 0)
                    return false;
                return true;
            }
        }
    }
}
