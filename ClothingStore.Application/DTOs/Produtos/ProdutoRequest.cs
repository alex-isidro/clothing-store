using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.DTOs.Produtos;

public sealed record ProdutoRequest(
    [Required(ErrorMessage = "O id da marca é obrigatório.")]
    Guid MarcaId,

    [Required(ErrorMessage = "O id da categoria é obrigatório.")]
    Guid CategoriaId,

    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
    [MaxLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
    string Descricao,

    [Range(0.01, 999999.99, ErrorMessage = "O preço deve ser maior que zero.")]
    decimal Preco,

    [Required(ErrorMessage = "O tamanho é obrigatório.")]
    [MaxLength(10, ErrorMessage = "O tamanho deve ter no máximo 10 caracteres.")]
    string Tamanho,

    [Required(ErrorMessage = "A cor é obrigatória.")]
    [MaxLength(50, ErrorMessage = "A cor deve ter no máximo 50 caracteres.")]
    string Cor);