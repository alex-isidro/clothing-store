using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class Cliente : BaseEntity
{
    public string Nome { get; set; }

    public string Cpf { get; set; }

    public string Email { get; set; }

    public string Telefone { get; set; }
    public DateTime DataCadastro { get; private set; }
    
    public Cliente()
    {
        DataCadastro = DateTime.UtcNow;
    }
}
