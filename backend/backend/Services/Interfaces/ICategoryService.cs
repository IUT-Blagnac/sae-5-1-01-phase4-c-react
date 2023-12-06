using backend.Data.Models;

namespace backend.Services.Interfaces;

public interface ICategoryService
{
    public List<Category> GetCategories();
}