using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome).IsRequired();
        builder.Property(p => p.Descricao).IsRequired();

        builder.Property(p => p.Preco)
            .HasColumnType("decimal(10,2)");

        builder.Property(p => p.Tamanho).IsRequired();
        builder.Property(p => p.Cor).IsRequired();

        // N:1
        builder.HasOne<Categoria>()
            .WithMany()
            .HasForeignKey(p => p.CategoriaId)
            .IsRequired();

        // N:1
        builder.HasOne<Marca>()
            .WithMany()
            .HasForeignKey(p => p.MarcaId)
            .IsRequired();
    }
}