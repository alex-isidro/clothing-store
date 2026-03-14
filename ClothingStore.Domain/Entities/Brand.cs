using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class Brand : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}