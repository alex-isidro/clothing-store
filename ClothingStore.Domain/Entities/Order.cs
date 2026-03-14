using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
}