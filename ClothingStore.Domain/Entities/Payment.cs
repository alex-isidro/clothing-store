namespace ClothingStore.Domain.Entities;

public class Payment
{
    public Guid Id { get; private set; }
    public string PaymentType { get; set; }
    public string PaymentStatus { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    public Guid OrderId { get; set; }

    public Payment()
    {
        Id = Guid.NewGuid();
    }
}