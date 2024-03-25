using Microsoft.AspNetCore.Mvc;
using QuangToys.CatalogService.Interfaces;
using QuangToys.CatalogService.Models;

namespace QuangToys.CatalogService.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productsRepo;
        private readonly ICategoryRepository _categoriesRepo;
        public ProductController(IProductRepository repository, ICategoryRepository categoriesRepo)
        {
            _productsRepo = repository;
            _categoriesRepo = categoriesRepo;

        }
        [HttpPost("create")]
        public async Task<IActionResult> HandleCreate(ProductCreateDto product)
        {
            var category = await _categoriesRepo.Get(Guid.Parse(product.CategoryId));
            if(category == null)
            {
                return Ok(false);
            }
            var newProduct = new Product()
            {
                Id = Guid.NewGuid(),
                Name = product.Name
            };
            var createResult = await _productsRepo.Add(newProduct);
            category.Products.Add(newProduct);
            var updateCategoryResult = await _categoriesRepo.Update(category);
            return Ok(createResult && updateCategoryResult);
        }

        [HttpPut("update")]
        public async Task<IActionResult> HandleUpdate(Product product)
        {
            var updateResult = await _productsRepo.Update(product);
            return Ok(updateResult);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> HandleDelete(Guid id)
        {
            var deleteResult = await _productsRepo.Delete(id);
            return Ok(deleteResult);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> HandleGet(Guid id)
        {
            var product = await _productsRepo.Get(id);
            return Ok(product);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> HandleGetList(int limit, int offset)
        {
            return Ok(await _productsRepo.GetAll());
        }

    }
}
