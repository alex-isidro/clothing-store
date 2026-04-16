using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome).IsRequired();
        builder.Property(c => c.Cpf).IsRequired();
        builder.Property(c => c.Email).IsRequired();
        builder.Property(c => c.Telefone).IsRequired();
        builder.Property(c => c.DataCadastro).IsRequired();

        builder.HasIndex(c => c.Cpf).IsUnique();
        builder.HasIndex(c => c.Email).IsUnique();
    }
}