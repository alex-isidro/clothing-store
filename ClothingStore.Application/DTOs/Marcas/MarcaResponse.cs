using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.DTOs.Marcas;

public sealed record MarcaResponse(
    Guid Id,
    string Nome,
    string Descricao,
    bool Active,
    DateTime CreatedAt)
{
    public static MarcaResponse FromEntity(Marca marca)
    {
        return new MarcaResponse(
            marca.Id,
            marca.Nome,
            marca.Descricao,
            marca.Active,
            marca.CreatedAt);
    }
}
