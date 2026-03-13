namespace ClothingStore.Domain.Entities;

public class Inventory
{
    public Guid Id { get; private set; }
    public int AvailableQuantity { get; set; }
    public int MinimumQuantity { get; set; }
    

    public Inventory()
    {
        Id = Guid.NewGuid();
    }
}