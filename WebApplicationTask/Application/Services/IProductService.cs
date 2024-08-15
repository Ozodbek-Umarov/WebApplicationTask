using WebApplicationTask.Application.DTOs.ProductDtos;

namespace WebApplicationTask.Application.Services;

public interface IProductService
{
    Task<ProductDto> CreateAsync(AddProductDto productDto);
    Task<List<ProductDto>> GetAllAsync();
    Task<ProductDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, AddProductDto dto);
    Task DeleteAsync(int id);
}
