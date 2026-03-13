namespace ClothingStore.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
    

    public Order()
    {
        Id = Guid.NewGuid();
    }
}