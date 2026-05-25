using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.DTOs.Produtos;

public sealed record ProdutoResponse(
    Guid Id,
    Guid MarcaId,
    Guid CategoriaId,
    string Nome,
    string Descricao,
    decimal Preco,
    string Tamanho,
    string Cor,
    bool Active,
    DateTime CreatedAt)
{
    public static ProdutoResponse FromEntity(Produto produto)
    {
        return new ProdutoResponse(
            produto.Id,
            produto.MarcaId,
            produto.CategoriaId,
            produto.Nome,
            produto.Descricao,
            produto.Preco,
            produto.Tamanho,
            produto.Cor,
            produto.Active,
            produto.CreatedAt);
    }
}
