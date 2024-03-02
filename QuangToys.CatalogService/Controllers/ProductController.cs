using Microsoft.AspNetCore.Mvc;
using QuangToys.CatalogService.Interfaces;
using QuangToys.CatalogService.Models;
using QuangToys.Common.Interfaces;

namespace QuangToys.CatalogService.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productsRepo;
        public ProductController(IProductRepository repository)
        {
            _productsRepo = repository;
        }
        [HttpPost("create")]
        public async Task<IActionResult> HandleCreate(Product product)
        {
            var createResult = await _productsRepo.Add(product);
            return Ok(createResult);
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
