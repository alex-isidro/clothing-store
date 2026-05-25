using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.DTOs.Pedidos;

public sealed record PedidoResponse(
    Guid Id,
    Guid ClienteId,
    Guid EnderecoEntregaId,
    DateTime DataPedido,
    string Status,
    decimal ValorTotal,
    IReadOnlyList<ItemPedidoResponse> Itens,
    bool Active,
    DateTime CreatedAt)
{
    public static PedidoResponse FromEntity(Pedido pedido)
    {
        return new PedidoResponse(
            pedido.Id,
            pedido.ClienteId,
            pedido.EnderecoEntregaId,
            pedido.DataPedido,
            pedido.Status,
            pedido.ValorTotal,
            pedido.Itens.Select(ItemPedidoResponse.FromEntity).ToList(),
            pedido.Active,
            pedido.CreatedAt);
    }
}
