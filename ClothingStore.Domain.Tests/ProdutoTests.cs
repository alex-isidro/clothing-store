using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Exceptions;
using Xunit;

namespace ClothingStore.Domain.Tests;

public class ProdutoTests
{
    [Fact]
    public void Produto_ComDadosValidos_DeveCriarProduto()
    {
        // Arrange
        var marcaId = Guid.NewGuid();
        var categoriaId = Guid.NewGuid();

        // Act
        var produto = new Produto(
            marcaId,
            categoriaId,
            "Camiseta Básica",
            "Camiseta de algodão",
            79.90m,
            "M",
            "Preta");

        // Assert
        Assert.Equal("Camiseta Básica", produto.Nome);
        Assert.Equal(79.90m, produto.Preco);
        Assert.Equal(marcaId, produto.MarcaId);
        Assert.Equal(categoriaId, produto.CategoriaId);
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Produto_ComPrecoNegativo_DeveLancarDomainException(double preco)
    {
        // Arrange
        var marcaId = Guid.NewGuid();
        var categoriaId = Guid.NewGuid();

        // Act
        var act = () => new Produto(
            marcaId,
            categoriaId,
            "Camiseta Básica",
            "Camiseta de algodão",
            (decimal)preco,
            "M",
            "Preta");

        // Assert
        var ex = Assert.Throws<DomainException>(act);
        Assert.Equal("Preço não pode ser negativo.", ex.Message);
    }
}