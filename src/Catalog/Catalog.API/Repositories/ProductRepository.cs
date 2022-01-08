using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogDbContext _catalogDbContext;
        public ProductRepository(ICatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        public async Task CreateProduct(Product product)
        {
            await _catalogDbContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _catalogDbContext.Products.DeleteOneAsync(filterDefinition);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            ReplaceOneResult result = await _catalogDbContext.Products.ReplaceOneAsync(filterDefinition, product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.ElemMatch(p => p.Category, category);
            var collection = await _catalogDbContext.Products.FindAsync<Product>(filterDefinition);
            return await collection.ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            var collection = await _catalogDbContext.Products.FindAsync(p => p.Id == id);
            return await collection.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            var collection = await _catalogDbContext.Products.FindAsync<Product>(filterDefinition);
            return await collection.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var collection = await _catalogDbContext.Products.FindAsync(p => true);
            return await collection.ToListAsync();
        }
    }
}
