using eCommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceAPI.Services.ProductService
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAll();
        public Task<IEnumerable<Product>> GetProductsByCategories(int[] categoryIds);
        public Task<IEnumerable<Product>> GetProductsByPrice(int filterPrice);
        public Task<Product> Get(int id);
        public Task<Product> AddProduct(Product newProduct, int[]? selectedCategoryIds);
        public Task<Product> UpdateProduct(Product updatedProduct);
        public Task<Product> DeleteProduct(int id);
    }
}
