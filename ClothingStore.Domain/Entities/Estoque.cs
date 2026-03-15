using ClothingStore.Domain.Commom;

namespace ClothingStore.Domain.Entities;

public class Estoque : BaseEntity
{
    public Guid ProdutoId { get; private set; }
    public int QuantidadeDisponivel { get; private set; }
    public int QuantidadeMinima { get; private set; }

    public Estoque(Guid produtoId, int quantidadeDisponivel, int quantidadeMinima)
    {
        if (quantidadeDisponivel < 0 || quantidadeMinima < 0)
            throw new Exception("Valores de estoque não podem ser negativos.");

        ProdutoId = produtoId;
        QuantidadeDisponivel = quantidadeDisponivel;
        QuantidadeMinima = quantidadeMinima;
    }
}