using ClothingStore.Domain.Commom;
using ClothingStore.Domain.Exceptions;

namespace ClothingStore.Domain.Entities;

public class Pedido : BaseEntity
{
    public Guid ClienteId { get; private set; }
    public Guid EnderecoEntregaId { get; private set; }
    public DateTime DataPedido { get; private set; }
    public string Status { get; private set; }
    public decimal ValorTotal { get; private set; }

    public List<ItemPedido> Itens { get; private set; }
    public Pagamento? Pagamento { get; private set; }

    protected Pedido()
    {
        Status = string.Empty;
        Itens = new List<ItemPedido>();
    }

    public Pedido(Guid clienteId, Guid enderecoEntregaId, DateTime dataPedido, string status, decimal valorTotal)
    {
        if (clienteId == Guid.Empty)
            throw new DomainException("ClienteId não pode ser vazio.");

        if (enderecoEntregaId == Guid.Empty)
            throw new DomainException("EnderecoEntregaId não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(status))
            throw new DomainException("Status não pode ser vazio.");

        if (valorTotal < 0)
            throw new DomainException("Valor total não pode ser negativo.");

        ClienteId = clienteId;
        EnderecoEntregaId = enderecoEntregaId;
        DataPedido = dataPedido;
        Status = status.Trim();
        ValorTotal = valorTotal;
        Itens = new List<ItemPedido>();
    }
}
