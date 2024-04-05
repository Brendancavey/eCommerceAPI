using eCommerceAPI.DBContext;
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
        [Route("get-products-by-filters")]
        public async Task<IActionResult> GetProductsByFilters([FromQuery] int[] selectedCategoryIds, int filterPrice, string sortOrder="asc")
        {
            return Ok(await _productService.GetProductsByFilters(selectedCategoryIds, filterPrice, sortOrder));
        }
        [HttpGet]
        [Route("get-products-by-categories")]
        public async Task<IActionResult> GetProductsByCategories([FromQuery] int[] selectedCategoryIds)
        {
            return Ok(await _productService.GetProductsByCategories(selectedCategoryIds));
        }
        [HttpGet]
        [Route("get-products-by-price")]
        public async Task<IActionResult> GetProductsByPrice(int filterPrice)
        {
            return Ok(await _productService.GetProductsByPrice(filterPrice));
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
        public async Task<IActionResult> AddProduct([FromForm] ProductViewModel productModel, IFormFile file0, IFormFile file1 = null)
        {
            var newProduct = new Product()
            {
                title = productModel.title,
                description = productModel.description,
                isNew = productModel.isNew,
                price = productModel.price,
                salePrice = productModel.salePrice,
            };
            if (file0 != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file0.CopyToAsync(memoryStream);
                    newProduct.img = memoryStream.ToArray();
                }
            }
            if (file1 != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file1.CopyToAsync(memoryStream);
                    newProduct.img2 = memoryStream.ToArray();
                }
            }
            return Ok(await _productService.AddProduct(newProduct, productModel.SelectedCategoryIds));
        }
        [HttpPut]
        [Route("updateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] Product updatedProduct, IFormFile file0 = null, IFormFile file1 = null) 
        {
            var existingProduct = await _productService.Get(updatedProduct.Id);
            existingProduct.title = updatedProduct.title;
            existingProduct.description = updatedProduct.description;
            existingProduct.isNew = updatedProduct.isNew;
            existingProduct.price = updatedProduct.price;
            existingProduct.salePrice  = updatedProduct.salePrice;
            if (file0 != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file0.CopyToAsync(memoryStream);
                    existingProduct.img = memoryStream.ToArray();
                }
            }
            if (file1 != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file1.CopyToAsync(memoryStream);
                    existingProduct.img2 = memoryStream.ToArray();
                }
            }
            return Ok(await _productService.UpdateProduct(existingProduct));
        }
        [HttpDelete]
        [Route("deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }
    }

}
