using eCommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceAPI.Services.ProductService
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAll();
        public Task<IEnumerable<Product>> GetProductsByFilters(int[] categoryIds, int filterPrice, string sortOrder);
        public Task<IEnumerable<Product>> GetProductsByCategories(int[] categoryIds);
        public Task<IEnumerable<Product>> GetProductsByPrice(int filterPrice);
        public IEnumerable<Product> GetProductsPriceSorted(IEnumerable<Product> products, string sortOrder);
        public Task<Product> Get(int id);
        public Task<Product> AddProduct(Product newProduct, int[]? selectedCategoryIds);
        public Task<Product> UpdateProduct(Product updatedProduct, int[]? selectedCategoryIds);
        public Task<Product> DeleteProduct(int id);
    }
}
