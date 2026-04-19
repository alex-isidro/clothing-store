using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories;

public interface IClienteRepository : IGenericRepository<Cliente>
{
    Task<Cliente?> GetByEmailAsync(string email);
    Task<Cliente?> GetByCpfAsync(string cpf);
    Task<Cliente?> GetWithPedidosAsync(Guid id);
}
