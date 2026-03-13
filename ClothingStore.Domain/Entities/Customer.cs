namespace ClothingStore.Domain.Entities;

public class Customer
{
    public Guid Id { get; private set; }

    public string Name { get; set; }

    public string Cpf { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }
    public DateTime RegistrationDate { get; private set; }
    
    public Customer()
    {
        Id = Guid.NewGuid();
        RegistrationDate = DateTime.UtcNow;
    }
}