using ClothingStore.Domain.Commom;
using ClothingStore.Domain.Exceptions;

namespace ClothingStore.Domain.Entities;

public class Produto : BaseEntity
{
    public Guid MarcaId { get; private set; }
    public Guid CategoriaId { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public string Tamanho { get; private set; }
    public string Cor { get; private set; }

    public Estoque? Estoque { get; private set; }
    public List<ItemPedido> ItensPedido { get; private set; }

    protected Produto()
    {
        Nome = string.Empty;
        Descricao = string.Empty;
        Tamanho = string.Empty;
        Cor = string.Empty;
        ItensPedido = new List<ItemPedido>();
    }

    public Produto(Guid marcaId, Guid categoriaId, string nome, string descricao, decimal preco, string tamanho, string cor)
    {
        if (marcaId == Guid.Empty)
            throw new DomainException("MarcaId não pode ser vazio.");

        if (categoriaId == Guid.Empty)
            throw new DomainException("CategoriaId não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("Descrição não pode ser vazia.");

        if (preco <= 0)
            throw new DomainException("Preço deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(tamanho))
            throw new DomainException("Tamanho não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(cor))
            throw new DomainException("Cor não pode ser vazia.");

        MarcaId = marcaId;
        CategoriaId = categoriaId;
        Nome = nome.Trim();
        Descricao = descricao.Trim();
        Preco = preco;
        Tamanho = tamanho.Trim();
        Cor = cor.Trim();
        ItensPedido = new List<ItemPedido>();
    }
}
