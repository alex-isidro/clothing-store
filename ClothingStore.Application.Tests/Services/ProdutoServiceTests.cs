using ClothingStore.Application.DTOs.Produtos;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Services;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Exceptions;
using Moq;
using Xunit;

namespace ClothingStore.Application.Tests.Services;

public class ProdutoServiceTests
{
    private readonly Mock<IProdutoRepository> _produtoRepository = new();
    private readonly Mock<IRepository<Marca>> _marcaRepository = new();
    private readonly Mock<IRepository<Categoria>> _categoriaRepository = new();
    private readonly ProdutoService _service;

    public ProdutoServiceTests()
    {
        _service = new ProdutoService(
            _produtoRepository.Object,
            _marcaRepository.Object,
            _categoriaRepository.Object);
    }

    [Fact]
    public async Task Create_QuandoCategoriaNaoExiste_DeveLancarNotFoundENaoPersistir()
    {
        // Arrange
        var request = new ProdutoRequest(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Camiseta Básica",
            "Camiseta de algodão",
            79.90m,
            "M",
            "Preta");

        _marcaRepository
            .Setup(r => r.ExistsAsync(request.MarcaId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _categoriaRepository
            .Setup(r => r.ExistsAsync(request.CategoriaId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var act = () => _service.CreateAsync(request);

        // Assert
        var ex = await Assert.ThrowsAsync<ResourceNotFoundException>(act);
        Assert.Equal($"Categoria com id '{request.CategoriaId}' não foi encontrado.", ex.Message);

        _produtoRepository.Verify(
            r => r.AddAsync(It.IsAny<Produto>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Create_ComDadosValidos_DevePersistirUmaVez()
    {
        // Arrange
        var request = new ProdutoRequest(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Camiseta Básica",
            "Camiseta de algodão",
            79.90m,
            "M",
            "Preta");

        _marcaRepository
            .Setup(r => r.ExistsAsync(request.MarcaId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _categoriaRepository
            .Setup(r => r.ExistsAsync(request.CategoriaId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var response = await _service.CreateAsync(request);

        // Assert
        Assert.Equal(request.Nome, response.Nome);
        Assert.Equal(request.Preco, response.Preco);

        _produtoRepository.Verify(
            r => r.AddAsync(It.IsAny<Produto>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
