using YourCommerce.Domain.Primitives;
using YourCommerce.Domain.ValueObjects;

namespace YourCommerce.Domain.Customers;

public sealed class Customer : AggregateRoot
{
    public Customer(CustomerId customerId, string name, string lastName, string email, PhoneNumber phoneNumber, Address address, bool active)
    {
        Id = customerId;
        Name = name;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        Active = active;
    }
    
    public Customer()
    {
    }

    public CustomerId Id {get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string FullName => $"{Name} {LastName}";

    public string Email { get; private set; } = string.Empty;
    public PhoneNumber PhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public bool Active { get; set; }

}