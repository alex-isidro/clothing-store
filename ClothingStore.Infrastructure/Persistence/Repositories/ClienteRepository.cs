using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class ClienteRepository(ClothingStoreContext context) : GenericRepository<Cliente>(context), IClienteRepository
{
    public async Task<Cliente?> GetByEmailAsync(string email)
    {
        var normalizedEmail = email.Trim();

        return await context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == normalizedEmail);
    }

    public async Task<Cliente?> GetByCpfAsync(string cpf)
    {
        var normalizedCpf = cpf.Trim();

        return await context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Cpf == normalizedCpf);
    }

    public async Task<Cliente?> GetWithPedidosAsync(Guid id)
    {
        return await context.Clientes
            .AsNoTracking()
            .Include(c => c.Pedidos)
                .ThenInclude(p => p.Itens)
                    .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
