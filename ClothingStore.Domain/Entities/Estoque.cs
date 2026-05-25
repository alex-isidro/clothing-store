using ClothingStore.Domain.Commom;
using ClothingStore.Domain.Exceptions;

namespace ClothingStore.Domain.Entities;

public class Estoque : BaseEntity
{
    public Guid ProdutoId { get; private set; }
    public int QuantidadeDisponivel { get; private set; }
    public int QuantidadeMinima { get; private set; }

    protected Estoque()
    {
    }

    public Estoque(Guid produtoId, int quantidadeDisponivel, int quantidadeMinima)
    {
        if (produtoId == Guid.Empty)
            throw new DomainException("ProdutoId não pode ser vazio.");

        if (quantidadeDisponivel < 0 || quantidadeMinima < 0)
            throw new DomainException("Valores de estoque não podem ser negativos.");

        ProdutoId = produtoId;
        QuantidadeDisponivel = quantidadeDisponivel;
        QuantidadeMinima = quantidadeMinima;
    }
}
