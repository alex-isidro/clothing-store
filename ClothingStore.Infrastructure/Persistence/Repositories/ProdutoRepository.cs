using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class ProdutoRepository(ClothingStoreContext context) : GenericRepository<Produto>(context), IProdutoRepository
{
    public Task<List<Produto>> GetByCategoriaAsync(Guid categoriaId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Produto>> GetByMarcaAsync(Guid marcaId)
    {
        throw new NotImplementedException();
    }
}
