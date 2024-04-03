using eCommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceAPI.Services.ProductService
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetAll();
        public Task<IEnumerable<Category>> GetCategoriesByProduct(int id);
        public Task<Category> Get(int Id);
        public Task<Category> AddCategory(Category newCategory);
        public Task<Category> UpdateCategory(Category updatedCategory);
        public Task<Category> DeleteCategory(int id);
    }
}
