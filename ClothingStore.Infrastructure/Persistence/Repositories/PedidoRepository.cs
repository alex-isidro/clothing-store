using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class PedidoRepository(ClothingStoreContext context) : GenericRepository<Pedido>(context), IPedidoRepository
{
    public Task<List<Pedido>> GetByClienteIdAsync(Guid clienteId)
    {
        throw new NotImplementedException();
    }

    public Task<Pedido?> GetWithItensAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Pedido>> GetByStatusAsync(string status)
    {
        throw new NotImplementedException();
    }
}
