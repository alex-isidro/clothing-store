using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class ItemPedido : BaseEntity
{
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
