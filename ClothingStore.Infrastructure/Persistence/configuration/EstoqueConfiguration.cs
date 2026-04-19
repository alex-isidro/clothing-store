using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EstoqueConfiguration : IEntityTypeConfiguration<Estoque>
{
    public void Configure(EntityTypeBuilder<Estoque> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.QuantidadeDisponivel)
            .IsRequired();

        builder.Property(e => e.QuantidadeMinima)
            .IsRequired();

        // 1:1
        builder.HasOne<Produto>()
            .WithOne(p => p.Estoque)
            .HasForeignKey<Estoque>(e => e.ProdutoId)
            .IsRequired();
    }
}