using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class ProdutoRepository(ClothingStoreContext context) : GenericRepository<Produto>(context), IProdutoRepository
{
    public async Task<List<Produto>> GetByCategoriaAsync(Guid categoriaId)
    {
        return await context.Produtos
            .AsNoTracking()
            .Where(p => p.CategoriaId == categoriaId)
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<List<Produto>> GetByMarcaAsync(Guid marcaId)
    {
        return await context.Produtos
            .AsNoTracking()
            .Where(p => p.MarcaId == marcaId)
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }
}
