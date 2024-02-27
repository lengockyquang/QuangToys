using Microsoft.AspNetCore.Mvc;
using QuangToys.CatalogService.Models;
using QuangToys.Common.Interfaces;

namespace QuangToys.CatalogService.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _categoryRepo;
        public CategoryController(IRepository<Category> repository)
        {
            _categoryRepo = repository;
        }
        [HttpPost("create")]
        public async Task<IActionResult> HandleCreate(Category category)
        {
            var createResult = await _categoryRepo.Add(category);
            return Ok(createResult);
        }

        [HttpPut("update")]
        public async Task<IActionResult> HandleUpdate(Category category)
        {
            var updateResult = await _categoryRepo.Update(category);
            return Ok(updateResult);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> HandleDelete(Guid id)
        {
            var deleteResult = await _categoryRepo.Delete(id);
            return Ok(deleteResult);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> HandleGet(Guid id)
        {
            var product = await _categoryRepo.Get(id);
            return Ok(product);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> HandleGetList(int limit, int offset)
        {
            return Ok(await _categoryRepo.GetAll());
        }
    }
}
