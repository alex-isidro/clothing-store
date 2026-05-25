using ClothingStore.Domain.Commom;

namespace ClothingStore.Application.Interfaces.Repositories;

public interface IGenericRepository<T> : IRepository<T> where T : BaseEntity
{
}
