using System.ComponentModel.DataAnnotations;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.DTOs;

public record ProdutoRequest(
    [Required(ErrorMessage = "O campo marcaId é obrigatório")]
    Guid marcaId,

    [Required(ErrorMessage = "O campo categoriaId é obrigatório")]
    Guid categoriaId,

    [Required(ErrorMessage = "O campo nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo nome deve ter entre 2 e 50 caracteres")]
    string nome,

    [Required(ErrorMessage = "O campo descricao é obrigatório")]
    [StringLength(300, MinimumLength = 2, ErrorMessage = "O campo descricao deve ter entre 2 e 150 caracteres")]
    string descricao,

    [Required(ErrorMessage = "O campo preço é obrigatório")]
    [Range(0.01, 999999, ErrorMessage = "O campo preço deve ser maior que zero")]
    decimal preco,

    [Required(ErrorMessage = "O campo tamanho é obrigatório")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "O campo tamanho deve ter entre 1 e 10 caracteres")]
    string tamanho,

    [Required(ErrorMessage = "O campo cor é obrigatório")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "O campo cor deve ter entre 2 e 15 caracteres")]
    string cor
)
{
    public Produto ToDomain() => new Produto(marcaId, categoriaId, nome, descricao, preco, tamanho, cor);
}