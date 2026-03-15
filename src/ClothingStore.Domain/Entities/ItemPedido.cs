using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class ItemPedido : BaseEntity
{
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public int Quantidade { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public decimal Subtotal { get; private set; }

    public ItemPedido(Guid pedidoId, Guid produtoId, int quantidade, decimal precoUnitario)
    {
        if (pedidoId == Guid.Empty)
            throw new Exception("PedidoId não pode ser vazio.");

        if (produtoId == Guid.Empty)
            throw new Exception("ProdutoId não pode ser vazio.");

        if (quantidade < 1)
            throw new Exception("Quantidade deve ser maior que zero.");

        if (precoUnitario < 0)
            throw new Exception("Preço unitário não pode ser negativo.");

        PedidoId = pedidoId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        Subtotal = quantidade * precoUnitario;
    }
}