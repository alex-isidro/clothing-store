using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class PedidoRepository(ClothingStoreContext context) : GenericRepository<Pedido>(context), IPedidoRepository
{
}
