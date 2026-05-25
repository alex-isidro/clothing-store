using ClothingStore.Domain.Commom;
using ClothingStore.Domain.Exceptions;

namespace ClothingStore.Domain.Entities;

public class Pagamento : BaseEntity
{
    public Guid PedidoId { get; private set; }
    public string TipoPagamento { get; private set; }
    public string StatusPagamento { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataPagamento { get; private set; }

    protected Pagamento()
    {
        TipoPagamento = string.Empty;
        StatusPagamento = string.Empty;
    }

    public Pagamento(Guid pedidoId, string tipoPagamento, string statusPagamento, decimal valor, DateTime dataPagamento)
    {
        if (pedidoId == Guid.Empty)
            throw new DomainException("PedidoId não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(tipoPagamento))
            throw new DomainException("Tipo de pagamento não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(statusPagamento))
            throw new DomainException("Status do pagamento não pode ser vazio.");

        if (valor < 0)
            throw new DomainException("Valor não pode ser negativo.");

        PedidoId = pedidoId;
        TipoPagamento = tipoPagamento.Trim();
        StatusPagamento = statusPagamento.Trim();
        Valor = valor;
        DataPagamento = dataPagamento;
    }
}
