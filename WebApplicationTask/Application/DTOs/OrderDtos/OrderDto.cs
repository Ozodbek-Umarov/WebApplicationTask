namespace WebApplicationTask.Application.DTOs.OrderDtos;

public class OrderDto
{
    public int Id { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public long TotalPrice { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
}
