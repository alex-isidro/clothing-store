using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class Pedido : BaseEntity
{
    public DateTime DataPedido { get; set; }
    public string Status { get; set; }
    public decimal ValorTotal { get; set; }
    
}
