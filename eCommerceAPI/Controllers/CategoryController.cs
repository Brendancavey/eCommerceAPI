using eCommerceAPI.Models;
using eCommerceAPI.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eCommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAll());
        }
        [HttpGet]
        [Route("get-categories-by-product/{id}")]
        public async Task<IActionResult> GetCategoriesByProduct(int productId)
        {
            return Ok(await _categoryService.GetCategoriesByProduct(productId));
        }
        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _categoryService.Get(id));
        }
        [HttpPost]
        [Route("addCategory")]
        public async Task<IActionResult> AddCategory([FromForm] Category newCategory)
        {
            return Ok(await _categoryService.AddCategory(newCategory));
        }
        [HttpPut]
        [Route("updateCategory")]
        public async Task<IActionResult> UpdateCategory([FromForm] Category updatedCategory) 
        {
            return Ok(await _categoryService.UpdateCategory(updatedCategory));
        }
        [HttpDelete]
        [Route("deleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return Ok(await _categoryService.DeleteCategory(id));
        }
    }

}
