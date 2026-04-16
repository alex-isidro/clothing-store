using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
{
    public void Configure(EntityTypeBuilder<Pagamento> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.TipoPagamento).IsRequired();
        builder.Property(p => p.StatusPagamento).IsRequired();

        builder.Property(p => p.Valor)
            .HasColumnType("decimal(10,2)");

        builder.Property(p => p.DataPagamento).IsRequired();

        // 1:1
        builder.HasOne<Pedido>()
            .WithOne(p => p.Pagamento)
            .HasForeignKey<Pagamento>(p => p.PedidoId)
            .IsRequired();
    }
}