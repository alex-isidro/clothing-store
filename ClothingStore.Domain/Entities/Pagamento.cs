using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class Pagamento : BaseEntity
{
    public string TipoPagamento { get; set; }
    public string StatusPagamento { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataPagamento { get; set; }
    
}
