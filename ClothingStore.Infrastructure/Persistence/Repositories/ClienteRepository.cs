using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class ClienteRepository(ClothingStoreContext context) : GenericRepository<Cliente>(context), IClienteRepository
{
}
