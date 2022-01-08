using Catalog.API.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogDbContext : ICatalogDbContext
    {
        private readonly CatalogDatabaseSettings _settings;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        public CatalogDbContext(IOptions<CatalogDatabaseSettings> settings)
        {
            _settings = settings.Value;
            _mongoClient = new MongoClient(_settings.ConnectionString);
            _database = _mongoClient.GetDatabase(_settings.DatabaseName);
        }

        public IMongoCollection<Product> Products
        {
            get { return _database.GetCollection<Product>(_settings.CollectionName); }
        }
    }
}
