using System.ComponentModel.DataAnnotations;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.DTOs;

public record ClienteRequest(
    [Required(ErrorMessage = "O campo nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo nome deve ter entre 2 e 50 caracteres")]
    string nome,

    [Required(ErrorMessage = "O campo cpf é obrigatório")]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo cpf deve ter entre 11 e 14 caracteres")]
    string cpf,

    [Required(ErrorMessage = "O campo email é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo email é inválido")]
    string email,

    [Required(ErrorMessage = "O campo telefone é obrigatório")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "O campo telefone deve ter entre 8 e 20 caracteres")]
    string telefone
)
{
    public Cliente ToDomain() => new Cliente(nome, cpf, email, telefone);
}