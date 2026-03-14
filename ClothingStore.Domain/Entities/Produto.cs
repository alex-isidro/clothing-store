using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class Produto : BaseEntity
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public string Tamanho { get; set; }
    public string Cor { get; set; }
}
