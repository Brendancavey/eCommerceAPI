using eCommerceAPI.Models;
using eCommerceAPI.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController( IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {  
            return Ok(await _productService.GetAll());
        }
        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productService.Get(id));
        }
        [HttpPost]
        [Route("addProduct")]
        public async Task<IActionResult> AddProduct(Product newProduct)
        {
            return Ok(await _productService.AddProduct(newProduct));
        }
        [HttpPut]
        [Route("updateProduct")]
        public async Task<IActionResult> UpdateProduct(Product updatedProduct) 
        { 
            return Ok(await _productService.UpdateProduct(updatedProduct));
        }
        [HttpDelete]
        [Route("deleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }
    }

}
