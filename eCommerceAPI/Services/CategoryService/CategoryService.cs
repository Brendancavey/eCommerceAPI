using eCommerceAPI.DBContext;
using eCommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceAPI.Services.ProductService
{
    public class CategoryService : ICategoryService
    {
        private readonly EcommerceDBContext _context;
        public CategoryService(EcommerceDBContext context) 
        { 
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }
        public async Task<IEnumerable<Category>> GetCategoriesByProduct(int id)
        {
            var categories = await _context.Categories
                .Where(c => c.Products.Any(p => p.Id == id))
                .ToListAsync();
            return categories;
        }
        public async Task<Category> Get(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
        public async Task<Category> AddCategory(Category newCategory)
        {
            _context.Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;

        }
        public async Task<Category> UpdateCategory(Category updatedCategory)
        {
            _context.Update(updatedCategory);
            await _context.SaveChangesAsync();
            return updatedCategory;

        }
        public async Task<Category> DeleteCategory(int id)
        {
            var categoryToDelete = await _context.Categories.FindAsync(id);
            if (categoryToDelete != null)
            {
                _context.Categories.Remove(categoryToDelete);
                await _context.SaveChangesAsync();
            }
            return categoryToDelete;
        }
    }
}
