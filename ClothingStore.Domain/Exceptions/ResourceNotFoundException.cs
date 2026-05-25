namespace ClothingStore.Domain.Exceptions;

public class ResourceNotFoundException : DomainException
{
    public ResourceNotFoundException(string resourceName, Guid id)
        : base($"{resourceName} com id '{id}' não foi encontrado.")
    {
        ResourceName = resourceName;
        Id = id;
    }

    public string ResourceName { get; }
    public Guid Id { get; }
}
