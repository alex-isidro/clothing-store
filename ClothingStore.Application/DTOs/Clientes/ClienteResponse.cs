using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.DTOs.Clientes;

public sealed record ClienteResponse(
    Guid Id,
    string Nome,
    string Cpf,
    string Email,
    string Telefone,
    DateTime DataCadastro,
    bool Active,
    DateTime CreatedAt)
{
    public static ClienteResponse FromEntity(Cliente cliente)
    {
        return new ClienteResponse(
            cliente.Id,
            cliente.Nome,
            cliente.Cpf,
            cliente.Email,
            cliente.Telefone,
            cliente.DataCadastro,
            cliente.Active,
            cliente.CreatedAt);
    }
}
