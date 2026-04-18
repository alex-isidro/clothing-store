using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class ClienteRepository(ClothingStoreContext context) : GenericRepository<Cliente>(context), IClienteRepository
{
    public Task<Cliente?> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Cliente?> GetByCpfAsync(string cpf)
    {
        throw new NotImplementedException();
    }

    public Task<Cliente?> GetWithPedidosAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
