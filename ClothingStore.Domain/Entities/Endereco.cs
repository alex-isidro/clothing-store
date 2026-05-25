using ClothingStore.Domain.Commom;
using ClothingStore.Domain.Exceptions;

namespace ClothingStore.Domain.Entities;

public class Endereco : BaseEntity
{
    public Guid ClienteId { get; private set; }
    public string Logradouro { get; private set; }
    public string Numero { get; private set; }
    public string? Complemento { get; private set; }
    public string Bairro { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }
    public string Cep { get; private set; }

    protected Endereco()
    {
        Logradouro = string.Empty;
        Numero = string.Empty;
        Bairro = string.Empty;
        Cidade = string.Empty;
        Estado = string.Empty;
        Cep = string.Empty;
    }

    public Endereco(
        Guid clienteId,
        string logradouro,
        string numero,
        string? complemento,
        string bairro,
        string cidade,
        string estado,
        string cep)
    {
        if (clienteId == Guid.Empty)
            throw new DomainException("ClienteId não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(logradouro))
            throw new DomainException("Logradouro não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(numero))
            throw new DomainException("Número não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(bairro))
            throw new DomainException("Bairro não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(cidade))
            throw new DomainException("Cidade não pode ser vazia.");

        if (string.IsNullOrWhiteSpace(estado))
            throw new DomainException("Estado não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(cep))
            throw new DomainException("CEP não pode ser vazio.");

        ClienteId = clienteId;
        Logradouro = logradouro.Trim();
        Numero = numero.Trim();
        Complemento = complemento?.Trim();
        Bairro = bairro.Trim();
        Cidade = cidade.Trim();
        Estado = estado.Trim();
        Cep = cep.Trim();
    }
}
