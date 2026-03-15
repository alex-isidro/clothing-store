using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class Categoria : BaseEntity
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    
    public List<Produto> Produtos { get; private set; }

    public Categoria(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
        Produtos = new List<Produto>();
    }
}