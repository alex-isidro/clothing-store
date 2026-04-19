using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class PedidoRepository(ClothingStoreContext context) : GenericRepository<Pedido>(context), IPedidoRepository
{
    public async Task<List<Pedido>> GetByClienteIdAsync(Guid clienteId)
    {
        return await context.Pedidos
            .AsNoTracking()
            .Where(p => p.ClienteId == clienteId)
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .OrderByDescending(p => p.DataPedido)
            .ToListAsync();
    }

    public async Task<Pedido?> GetWithItensAsync(Guid id)
    {
        return await context.Pedidos
            .AsNoTracking()
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .Include(p => p.Pagamento)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Pedido>> GetByStatusAsync(string status)
    {
        var normalizedStatus = status.Trim().ToLower();

        return await context.Pedidos
            .AsNoTracking()
            .Where(p => p.Status.ToLower() == normalizedStatus)
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .OrderByDescending(p => p.DataPedido)
            .ToListAsync();
    }
}
