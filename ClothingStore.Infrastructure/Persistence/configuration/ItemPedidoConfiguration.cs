using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
{
    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Quantidade).IsRequired();

        builder.Property(i => i.PrecoUnitario)
            .HasColumnType("decimal(10,2)");

        builder.Property(i => i.Subtotal)
            .HasColumnType("decimal(10,2)");

        // N:1
        builder.HasOne<Pedido>()
            .WithMany()
            .HasForeignKey(i => i.PedidoId)
            .IsRequired();

        // N:1
        builder.HasOne<Produto>()
            .WithMany()
            .HasForeignKey(i => i.ProdutoId)
            .IsRequired();
    }
}