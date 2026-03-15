namespace ClothingStore.Domain.Commom;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    
}