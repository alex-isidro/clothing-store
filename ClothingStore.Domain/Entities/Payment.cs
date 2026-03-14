using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class Payment : BaseEntity
{
    public string PaymentType { get; set; }
    public string PaymentStatus { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    public Guid OrderId { get; set; }
}