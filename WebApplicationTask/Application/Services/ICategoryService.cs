using WebApplicationTask.Application.DTOs.CategoryDtos;

namespace WebApplicationTask.Application.Services;

public interface ICategoryService
{
    Task<CategoryDto> CreateAsync(AddCategoryDto categoryDto);
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, AddCategoryDto dto);
    Task DeleteAsync(int id);
}
