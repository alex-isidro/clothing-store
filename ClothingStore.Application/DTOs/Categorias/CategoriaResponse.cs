using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.DTOs.Categorias;

public sealed record CategoriaResponse(
    Guid Id,
    string Nome,
    string Descricao,
    bool Active,
    DateTime CreatedAt)
{
    public static CategoriaResponse FromEntity(Categoria categoria)
    {
        return new CategoriaResponse(
            categoria.Id,
            categoria.Nome,
            categoria.Descricao,
            categoria.Active,
            categoria.CreatedAt);
    }
}
