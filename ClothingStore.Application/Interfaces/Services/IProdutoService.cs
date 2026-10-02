using ClothingStore.Application.DTOs.Produtos;
using ClothingStore.Application.DTOs;

namespace ClothingStore.Application.Interfaces.Services;

public interface IProdutoService
{
    Task<ProdutoResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProdutoResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<ProdutoResponse> Items, int TotalItems)> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<ProdutoResponse> CreateAsync(
        ProdutoRequest request,
        CancellationToken cancellationToken = default);
}