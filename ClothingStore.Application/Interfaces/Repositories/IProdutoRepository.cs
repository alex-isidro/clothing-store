using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories;

public interface IProdutoRepository : IGenericRepository<Produto>
{
    Task<List<Produto>> GetByCategoriaAsync(Guid categoriaId);
    Task<List<Produto>> GetByMarcaAsync(Guid marcaId);
}
