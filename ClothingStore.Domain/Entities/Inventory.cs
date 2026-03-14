using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class Inventory : BaseEntity
{
    public int AvailableQuantity { get; set; }
    public int MinimumQuantity { get; set; }
}