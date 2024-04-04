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
        public async Task<IEnumerable<Product>> GetProductsByFilters(int[] categoryIds, int filterPrice, string sortOrder)
        {
            var filteredProducts = await GetProductsByPrice(filterPrice);
            if(categoryIds.Length > 0)
            {
                var productsByCategories = await GetProductsByCategories(categoryIds);
                filteredProducts = filteredProducts.Intersect(productsByCategories);
            }
            filteredProducts = GetProductsPriceSorted(filteredProducts, sortOrder);
            return filteredProducts;
        }
        public async Task<IEnumerable<Product>> GetProductsByCategories(int[] categoryIds)
        {
            var products = await _context.Products
                .Where(p => p.Categories.Any(c => categoryIds.Contains(c.Id)))
                .ToListAsync();
            return products;
        }
        public async Task<IEnumerable<Product>> GetProductsByPrice(int filterPrice)
        {
            var products = await _context.Products
                .Where(p => p.salePrice <= filterPrice)
                .ToListAsync();
            return products;
        }
        public IEnumerable<Product> GetProductsPriceSorted(IEnumerable<Product> products, string sortOrder) //sort order is desc or asc by price
        {
            if (sortOrder == "desc")
            {
                products = products.OrderByDescending(p => p.salePrice);
            }
            else if(sortOrder == "asc")
            {
                products = products.OrderBy(p => p.salePrice);
            }
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
