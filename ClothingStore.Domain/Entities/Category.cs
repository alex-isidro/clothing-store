namespace ClothingStore.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Category()
    {
        Id = Guid.NewGuid();
    }
}