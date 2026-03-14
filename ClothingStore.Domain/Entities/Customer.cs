using ClothingStore.Domain.Common;

namespace ClothingStore.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; }

    public string Cpf { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }
    public DateTime RegistrationDate { get; private set; }
    
    public Customer()
    {
        RegistrationDate = DateTime.UtcNow;
    }
}