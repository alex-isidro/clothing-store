using ClothingStore.Application.DTOs.Produtos;

namespace ClothingStore.Application.Interfaces.Services;

public interface IProdutoService
{
    Task<ProdutoResponse> CreateAsync(
        ProdutoRequest request,
        CancellationToken cancellationToken = default);
}
