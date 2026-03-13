namespace ClothingStore.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    

    public OrderItem()
    {
        Id = Guid.NewGuid();
    }
}