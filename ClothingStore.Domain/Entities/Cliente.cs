using ClothingStore.Domain.Commom;

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

    public Cliente(string nome, string cpf, string email, string telefone)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new Exception("Nome não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(cpf))
            throw new Exception("Cpf não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new Exception("E-mail inválido.");

        if (string.IsNullOrWhiteSpace(telefone))
            throw new Exception("Telefone não pode ser vazio.");

        Nome = nome;
        Cpf = cpf;
        Email = email;
        Telefone = telefone;
        DataCadastro = DateTime.UtcNow;
        Enderecos = new List<Endereco>();
        Pedidos = new List<Pedido>();
    }
}