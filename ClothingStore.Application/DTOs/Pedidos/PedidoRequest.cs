using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.DTOs.Pedidos;

public sealed record PedidoRequest(
    [Required(ErrorMessage = "O id do cliente é obrigatório.")]
    Guid ClienteId,

    [Required(ErrorMessage = "O id do endereço de entrega é obrigatório.")]
    Guid EnderecoEntregaId,

    [Required(ErrorMessage = "O status do pedido é obrigatório.")]
    [MaxLength(50, ErrorMessage = "O status deve ter no máximo 50 caracteres.")]
    string Status,

    [Range(0, 999999.99, ErrorMessage = "O valor total não pode ser negativo.")]
    decimal ValorTotal,

    DateTime? DataPedido);
