using ClothingStore.Domain.Common;

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

    public Produto(Guid marcaId, Guid categoriaId, string nome, string descricao, decimal preco, string tamanho, string cor)
    {
        if (marcaId == Guid.Empty)
            throw new Exception("MarcaId não pode ser vazio.");

        if (categoriaId == Guid.Empty)
            throw new Exception("CategoriaId não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(nome))
            throw new Exception("Nome não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(descricao))
            throw new Exception("Descrição não pode ser vazia.");

        if (preco < 0)
            throw new Exception("Preço não pode ser negativo.");

        if (string.IsNullOrWhiteSpace(tamanho))
            throw new Exception("Tamanho não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(cor))
            throw new Exception("Cor não pode ser vazia.");

        MarcaId = marcaId;
        CategoriaId = categoriaId;
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Tamanho = tamanho;
        Cor = cor;
        ItensPedido = new List<ItemPedido>();
    }
}