using ClothingStore.Domain.Commom;
using ClothingStore.Domain.Exceptions;

namespace ClothingStore.Domain.Entities;

public class Cliente : BaseEntity
{
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }
    public DateTime DataCadastro { get; private set; }

    public List<Endereco> Enderecos { get; private set; }
    public List<Pedido> Pedidos { get; private set; }

    protected Cliente()
    {
        Nome = string.Empty;
        Cpf = string.Empty;
        Email = string.Empty;
        Telefone = string.Empty;
        Enderecos = new List<Endereco>();
        Pedidos = new List<Pedido>();
    }

    public Cliente(string nome, string cpf, string email, string telefone)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(cpf))
            throw new DomainException("Cpf não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new DomainException("E-mail inválido.");

        if (string.IsNullOrWhiteSpace(telefone))
            throw new DomainException("Telefone não pode ser vazio.");

        Nome = nome.Trim();
        Cpf = cpf.Trim();
        Email = email.Trim();
        Telefone = telefone.Trim();
        DataCadastro = DateTime.UtcNow;
        Enderecos = new List<Endereco>();
        Pedidos = new List<Pedido>();
    }
}
