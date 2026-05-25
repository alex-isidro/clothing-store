using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Application.DTOs.Clientes;

public sealed record ClienteRequest(
    [property: Required(ErrorMessage = "O nome do cliente é obrigatório.")]
    [property: MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    string Nome,

    [property: Required(ErrorMessage = "O CPF é obrigatório.")]
    [property: StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter 11 dígitos, sem pontuação.")]
    string Cpf,

    [property: Required(ErrorMessage = "O e-mail é obrigatório.")]
    [property: EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
    [property: MaxLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres.")]
    string Email,

    [property: Required(ErrorMessage = "O telefone é obrigatório.")]
    [property: MaxLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
    string Telefone);
