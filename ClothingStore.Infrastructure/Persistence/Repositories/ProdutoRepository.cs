using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class ProdutoRepository(ClothingStoreContext context) : GenericRepository<Produto>(context), IProdutoRepository
{
}
