using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.Telefone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.DataCadastro)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.HasIndex(c => c.Cpf).IsUnique();
        builder.HasIndex(c => c.Email).IsUnique();
    }
}