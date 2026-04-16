using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.DataPedido).IsRequired();
        builder.Property(p => p.Status).IsRequired();

        builder.Property(p => p.ValorTotal)
            .HasColumnType("decimal(10,2)");

        // N:1
        builder.HasOne<Cliente>()
            .WithMany(c => c.Pedidos)
            .HasForeignKey(p => p.ClienteId)
            .IsRequired();

        // N:1
        builder.HasOne<Endereco>()
            .WithMany()
            .HasForeignKey(p => p.EnderecoEntregaId)
            .IsRequired();
    }
}