using ClothingStore.Application.DTOs.Produtos;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Exceptions;

namespace ClothingStore.Application.Services;

public sealed class ProdutoService(
    IProdutoRepository produtoRepository,
    IRepository<Marca> marcaRepository,
    IRepository<Categoria> categoriaRepository) : IProdutoService
{
    public async Task<ProdutoResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var produto = await produtoRepository.GetByIdAsync(id, cancellationToken);
        return produto is null ? null : ProdutoResponse.FromEntity(produto);
    }

    public async Task<IReadOnlyList<ProdutoResponse>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var produtos = await produtoRepository.GetAllAsync(cancellationToken);
        return produtos.Select(ProdutoResponse.FromEntity).ToList();
    }

    public async Task<(IReadOnlyList<ProdutoResponse> Items, int TotalItems)> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (page < 1)
            throw new ArgumentException("page deve ser maior ou igual a 1.", nameof(page));

        if (pageSize is < 1 or > 100)
            throw new ArgumentException("pageSize deve estar entre 1 e 100.", nameof(pageSize));

        var (items, totalItems) = await produtoRepository.GetPagedAsync(
            page, pageSize, cancellationToken);

        return (
            items.Select(ProdutoResponse.FromEntity).ToList(),
            totalItems);
    }

    public async Task<ProdutoResponse> CreateAsync(
        ProdutoRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!await marcaRepository.ExistsAsync(request.MarcaId, cancellationToken))
        {
            throw new ResourceNotFoundException("Marca", request.MarcaId);
        }

        if (!await categoriaRepository.ExistsAsync(request.CategoriaId, cancellationToken))
        {
            throw new ResourceNotFoundException("Categoria", request.CategoriaId);
        }

        var produto = new Produto(
            request.MarcaId,
            request.CategoriaId,
            request.Nome,
            request.Descricao,
            request.Preco,
            request.Tamanho,
            request.Cor);

        await produtoRepository.AddAsync(produto, cancellationToken);

        return ProdutoResponse.FromEntity(produto);
    }
}
