using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Commom;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly ClothingStoreContext Context;
    private readonly DbSet<T> _dbSet;

    public Repository(ClothingStoreContext context)
    {
        Context = context;
        _dbSet = Context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .OrderBy(entity => entity.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<T> Items, int TotalItems)> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
       
        var query = _dbSet
            .AsNoTracking();

        var totalItems = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(entity => entity.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalItems);
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
        return await _dbSet
            .AsNoTracking()
            .AnyAsync(entity => entity.Id == id, cancellationToken);
    }
}
