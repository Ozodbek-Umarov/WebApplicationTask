namespace WebApplicationTask.Data.Entities;

public class Order : BaseEntity
{
    public int Count { get; set; }
    public int Price { get; set; }
    public long TotalPrice { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
