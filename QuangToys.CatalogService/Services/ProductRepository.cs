using Microsoft.Extensions.Options;
using QuangToys.CatalogService.Interfaces;
using QuangToys.CatalogService.Models;
using QuangToys.Common.Models;
using QuangToys.Common.Services;

namespace QuangToys.CatalogService.Services
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private const string containerName = "Product";
        public ProductRepository(IOptions<CosmosSettings> cosmosSettings, ILogger<ProductRepository> logger): base(cosmosSettings, logger, containerName)
        {
        }
        
    }
}
