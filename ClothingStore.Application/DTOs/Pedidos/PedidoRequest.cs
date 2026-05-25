using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.DTOs.Pedidos;

public sealed record PedidoRequest(
    [property: Required(ErrorMessage = "O id do cliente é obrigatório.")]
    Guid ClienteId,

    [property: Required(ErrorMessage = "O id do endereço de entrega é obrigatório.")]
    Guid EnderecoEntregaId,

    [property: Required(ErrorMessage = "O status do pedido é obrigatório.")]
    [property: MaxLength(50, ErrorMessage = "O status deve ter no máximo 50 caracteres.")]
    string Status,

    [property: Range(0, 999999.99, ErrorMessage = "O valor total não pode ser negativo.")]
    decimal ValorTotal,

    DateTime? DataPedido);
