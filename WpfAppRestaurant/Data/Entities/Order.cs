namespace WpfAppRestaurant.Data.Entities;

public class Order
{
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();

    public void AddItem(Product product, int quantity)
    {
        var existingItem = Items.FirstOrDefault(item => item.Product.Id == product.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            Items.Add(new OrderItem { Product = product, Quantity = quantity });
        }
    }
}

public class OrderItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
}
