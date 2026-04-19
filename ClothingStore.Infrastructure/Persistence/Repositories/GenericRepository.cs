using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Commom;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ClothingStoreContext Context;
    private readonly DbSet<T> _dbSet;

    protected GenericRepository(ClothingStoreContext context)
    {
        Context = context;
        _dbSet = Context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
        Context.SaveChanges();
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
        Context.SaveChanges();
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().AnyAsync(entity => entity.Id == id, cancellationToken);
    }
}
