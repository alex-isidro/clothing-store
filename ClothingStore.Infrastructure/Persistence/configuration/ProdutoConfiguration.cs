using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(p => p.Preco)
            .HasColumnType("decimal(10,2)");

        builder.Property(p => p.Tamanho)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(p => p.Cor)
            .IsRequired()
            .HasMaxLength(50);

        // N:1
        builder.HasOne<Categoria>()
            .WithMany(c => c.Produtos)
            .HasForeignKey(p => p.CategoriaId)
            .IsRequired();
        // N:1
        builder.HasOne<Marca>()
            .WithMany(m => m.Produtos)
            .HasForeignKey(p => p.MarcaId)
            .IsRequired();
    }
}