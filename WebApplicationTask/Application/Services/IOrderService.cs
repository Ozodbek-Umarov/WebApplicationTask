using WebApplicationTask.Application.DTOs.OrderDtos;

namespace WebApplicationTask.Application.Services;

public interface IOrderService
{
    Task<OrderDto> CreateAsync(CreateOrderDto orderDto);
    Task<List<OrderDto>> GetAllAsync();
    Task<OrderDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, CreateOrderDto dto);
    Task DeleteAsync(int id);
}
