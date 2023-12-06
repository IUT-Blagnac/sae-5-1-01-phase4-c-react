using backend.Data;
using backend.Data.Models;
using backend.Services.Interfaces;

namespace backend.Services.Class
{
    public class CategoryService : ICategoryService
    {
        private readonly EntityContext _context;
        public CategoryService(EntityContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
