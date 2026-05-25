using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.DTOs.Pedidos;

public sealed record ItemPedidoResponse(
    Guid Id,
    Guid ProdutoId,
    string? ProdutoNome,
    int Quantidade,
    decimal PrecoUnitario,
    decimal Subtotal)
{
    public static ItemPedidoResponse FromEntity(ItemPedido item)
    {
        return new ItemPedidoResponse(
            item.Id,
            item.ProdutoId,
            item.Produto?.Nome,
            item.Quantidade,
            item.PrecoUnitario,
            item.Subtotal);
    }
}
