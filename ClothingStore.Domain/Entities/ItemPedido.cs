using ClothingStore.Domain.Commom;
using ClothingStore.Domain.Exceptions;

namespace ClothingStore.Domain.Entities;

public class ItemPedido : BaseEntity
{
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }

    public Produto Produto { get; private set; } = null!;
    public Pedido Pedido { get; private set; } = null!;
    public int Quantidade { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public decimal Subtotal { get; private set; }

    protected ItemPedido()
    {
    }

    public ItemPedido(Guid pedidoId, Guid produtoId, int quantidade, decimal precoUnitario)
    {
        if (pedidoId == Guid.Empty)
            throw new DomainException("PedidoId não pode ser vazio.");

        if (produtoId == Guid.Empty)
            throw new DomainException("ProdutoId não pode ser vazio.");

        if (quantidade < 1)
            throw new DomainException("Quantidade deve ser maior que zero.");

        if (precoUnitario < 0)
            throw new DomainException("Preço unitário não pode ser negativo.");

        PedidoId = pedidoId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        Subtotal = quantidade * precoUnitario;
    }
}
