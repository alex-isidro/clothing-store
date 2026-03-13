namespace ClothingStore.Domain.Entities;

public class Address
{
    public Guid Id { get; private set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Cep { get; set; }

    public Address()
    {
        Id = Guid.NewGuid();
    }
}