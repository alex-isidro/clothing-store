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
