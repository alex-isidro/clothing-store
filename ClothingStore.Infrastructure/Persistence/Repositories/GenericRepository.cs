using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Commom;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : Repository<T>, IGenericRepository<T> where T : BaseEntity
{
    public GenericRepository(ClothingStoreContext context) : base(context)
    {
    }
}
