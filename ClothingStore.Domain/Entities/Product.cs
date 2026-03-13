namespace ClothingStore.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    

    public Product()
    {
        Id = Guid.NewGuid();
    }
}