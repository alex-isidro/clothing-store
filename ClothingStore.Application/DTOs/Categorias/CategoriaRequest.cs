using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.DTOs.Categorias;

public sealed record CategoriaRequest(
    [property: Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [property: MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    string Nome,

    [property: Required(ErrorMessage = "A descrição da categoria é obrigatória.")]
    [property: MaxLength(300, ErrorMessage = "A descrição deve ter no máximo 300 caracteres.")]
    string Descricao);
