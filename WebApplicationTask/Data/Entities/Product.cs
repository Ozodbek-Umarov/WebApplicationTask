namespace WebApplicationTask.Data.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;  // To‘liq URL yoki nisbiy yo‘l
    public int Price { get; set; }
    public int Count { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}
