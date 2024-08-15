namespace WebApplicationTask.Application.DTOs.ProductDtos;

public class AddProductDto
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Count { get; set; }
    public int Price { get; set; }
    public int CategoryId { get; set; }
    public IFormFile Image { get; set; }
}
