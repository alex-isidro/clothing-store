using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.DTOs.Clientes;

public sealed record ClienteRequest(
    [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter 11 dígitos, sem pontuação.")]
    string Cpf,

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
    [MaxLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres.")]
    string Email,

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [MaxLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
    string Telefone);