using ClothingStore.Domain.Commom;
using ClothingStore.Domain.Exceptions;

namespace ClothingStore.Domain.Entities;

public class Categoria : BaseEntity
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public List<Produto> Produtos { get; private set; }

    protected Categoria()
    {
        Nome = string.Empty;
        Descricao = string.Empty;
        Produtos = new List<Produto>();
    }

    public Categoria(string nome, string descricao)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome da categoria não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("Descrição da categoria não pode ser vazia.");

        Nome = nome.Trim();
        Descricao = descricao.Trim();
        Produtos = new List<Produto>();
    }
}
