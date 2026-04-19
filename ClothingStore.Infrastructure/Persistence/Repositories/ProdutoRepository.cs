using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(ClothingStoreContext context) : base(context)
    {
    }

    public async Task<List<Produto>> GetByCategoriaAsync(Guid categoriaId)
    {
        return await Context.Produtos
            .AsNoTracking()
            .Where(p => p.CategoriaId == categoriaId)
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<List<Produto>> GetByMarcaAsync(Guid marcaId)
    {
        return await Context.Produtos
            .AsNoTracking()
            .Where(p => p.MarcaId == marcaId)
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }
}
