using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class Estoque : BaseEntity
{
    public int QuantidadeDisponivel { get; set; }
    public int QuantidadeMinima { get; set; }
}
