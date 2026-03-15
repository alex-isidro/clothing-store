using ClothingStore.Domain.Commom;

namespace ClothingStore.Domain.Entities;

public class Pagamento : BaseEntity
{
    public Guid PedidoId { get; private set; }
    public string TipoPagamento { get; private set; }
    public string StatusPagamento { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataPagamento { get; private set; }

    public Pagamento(Guid pedidoId, string tipoPagamento, string statusPagamento, decimal valor, DateTime dataPagamento)
    {
        if (pedidoId == Guid.Empty)
            throw new Exception("PedidoId não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(tipoPagamento))
            throw new Exception("Tipo de pagamento não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(statusPagamento))
            throw new Exception("Status do pagamento não pode ser vazio.");

        if (valor < 0)
            throw new Exception("Valor não pode ser negativo.");

        PedidoId = pedidoId;
        TipoPagamento = tipoPagamento;
        StatusPagamento = statusPagamento;
        Valor = valor;
        DataPagamento = dataPagamento;
    }
}