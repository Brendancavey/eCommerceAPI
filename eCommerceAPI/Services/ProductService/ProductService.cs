using eCommerceAPI.DBContext;
using eCommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceAPI.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly EcommerceDBContext _context;
        public ProductService(EcommerceDBContext context) 
        { 
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }
        public async Task<IEnumerable<Product>> GetProductsByCategories(int[] categoryIds)
        {
            var products = await _context.Products
                .Where(p => p.Categories.Any(c => categoryIds.Contains(c.Id)))
                .ToListAsync();
            return products;
        }
        public async Task<Product> Get(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<Product> AddProduct(Product newProduct, int[]? selectedCategoryIds)
        {
            var selectedCategories = _context.Categories
                .Where(c => selectedCategoryIds.Contains(c.Id))
                .ToList();
            newProduct.Categories = selectedCategories;
            _context.Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;

        }
        public async Task<Product> UpdateProduct(Product updatedProduct)
        {
            _context.Update(updatedProduct);
            await _context.SaveChangesAsync();
            return updatedProduct;

        }
        public async Task<Product> DeleteProduct(int id)
        {
            var productToDelete = await _context.Products.FindAsync(id);
            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
                await _context.SaveChangesAsync();
            }
            return productToDelete;
        }
    }
}
