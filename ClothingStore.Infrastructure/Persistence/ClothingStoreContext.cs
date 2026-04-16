using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence;

public class ClothingStoreContext : DbContext
{
    public ClothingStoreContext(DbContextOptions<ClothingStoreContext> optionsStore) : base(optionsStore)
    {
        
    }
    
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Estoque> Estoques { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClothingStoreContext).Assembly);
    }
}