using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.DTOs.Categorias;

public sealed record CategoriaRequest(
    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "A descrição da categoria é obrigatória.")]
    [MaxLength(300, ErrorMessage = "A descrição deve ter no máximo 300 caracteres.")]
    string Descricao);
