using WpfAppTask.Data.Entities;

namespace WpfAppTask.Services;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int id);
    Task CreateCategoryAsync(Category category);
    Task UpdateCategoryAsync(int id, Category category);
    Task DeleteCategoryAsync(int id);
}
