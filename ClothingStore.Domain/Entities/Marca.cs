using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class Marca : BaseEntity
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
}
