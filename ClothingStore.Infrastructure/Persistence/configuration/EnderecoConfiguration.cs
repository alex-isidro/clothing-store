using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Logradouro).IsRequired();
        builder.Property(e => e.Numero).IsRequired();
        builder.Property(e => e.Bairro).IsRequired();
        builder.Property(e => e.Cidade).IsRequired();
        builder.Property(e => e.Estado).IsRequired();
        builder.Property(e => e.Cep).IsRequired();

        // N:1
        builder.HasOne<Cliente>()
            .WithMany(c => c.Enderecos)
            .HasForeignKey(e => e.ClienteId)
            .IsRequired();
    }
}