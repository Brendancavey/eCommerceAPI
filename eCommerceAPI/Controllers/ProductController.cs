using eCommerceAPI.Models;
using eCommerceAPI.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eCommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
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
        [HttpGet]
        [Route("get-image-by-id/{productId}")]
        public async Task<IActionResult> GetImage(int productId)
        {
            var product = await _productService.Get(productId);
            byte[] imgData = product.img;
            return File(imgData, "image/jpg");
        }
        [HttpGet]
        [Route("get-image-by-id2/{productId}")]
        public async Task<IActionResult> GetImage2(int productId)
        {
            var product = await _productService.Get(productId);
            byte[] imgData = product.img2;
            return File(imgData, "image/jpg");
        }
        [HttpPost]
        [Route("addProduct")]
        public async Task<IActionResult> AddProduct(IFormFile file0, IFormFile file1, [FromForm] Product newProduct)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file0.CopyToAsync(memoryStream);
                newProduct.img = memoryStream.ToArray();
            }
            using (var memoryStream = new MemoryStream())
            {
                await file1.CopyToAsync(memoryStream);
                newProduct.img2 = memoryStream.ToArray();
            }
                return Ok(await _productService.AddProduct(newProduct));
        }
        [HttpPut]
        [Route("updateProduct")]
        public async Task<IActionResult> UpdateProduct(IFormFile file0, IFormFile file1, [FromForm] Product updatedProduct) 
        {
            using (var memoryStream = new MemoryStream())
            {
                await file0.CopyToAsync(memoryStream);
                updatedProduct.img = memoryStream.ToArray();
            }
            using (var memoryStream = new MemoryStream())
            {
                await file1.CopyToAsync(memoryStream);
                updatedProduct.img2 = memoryStream.ToArray();
            }
            return Ok(await _productService.UpdateProduct(updatedProduct));
        }
        [HttpDelete]
        [Route("deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }
    }

}
