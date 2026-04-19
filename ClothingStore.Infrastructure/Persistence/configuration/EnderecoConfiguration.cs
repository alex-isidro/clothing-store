using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Logradouro)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(e => e.Numero)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(e => e.Bairro)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Cidade)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Estado)
            .IsRequired()
            .HasMaxLength(2);

        builder.Property(e => e.Cep)
            .IsRequired()
            .HasMaxLength(8);

        // N:1
        builder.HasOne<Cliente>()
            .WithMany(c => c.Enderecos)
            .HasForeignKey(e => e.ClienteId)
            .IsRequired();
    }
}