using Microsoft.Extensions.Options;
using QuangToys.CatalogService.Models;
using QuangToys.Common.Models;
using QuangToys.Common.Services;

namespace QuangToys.CatalogService.Services
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(IOptions<CosmosSettings> cosmosSettings, ILogger<CategoryRepository> logger): base(cosmosSettings, logger, "Category")
        {

        }
    }
}
