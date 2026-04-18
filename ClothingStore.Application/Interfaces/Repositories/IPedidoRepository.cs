using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories;

public interface IPedidoRepository : IGenericRepository<Pedido>
{
    Task<List<Pedido>> GetByClienteIdAsync(Guid clienteId);
    Task<Pedido?> GetWithItensAsync(Guid id);
    Task<List<Pedido>> GetByStatusAsync(string status);
}
