namespace ClothingStore.Domain.Entities;

public class Brand
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Brand()
    {
        Id = Guid.NewGuid();
    }
}