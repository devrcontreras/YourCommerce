namespace YourCommerce.Domain.ValueObjects;

public partial record Address
{

    public Address(string country, string street1, string street2, string city, string state, string zipCode)
    {
        Country = country;
        Street1 = street1;
        Street2 = street2;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public string Country { get; init; }
    public string Street1 { get; init; }
    public string Street2 { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }

    public static Address? Create(string country, string street1, string street2, string city, string state, string zipCode)
    {

        if(string.IsNullOrEmpty(country) || string.IsNullOrEmpty(street1) ||
            string.IsNullOrEmpty(street2) || string.IsNullOrEmpty(city) ||
            string.IsNullOrEmpty(state) || string.IsNullOrEmpty(zipCode))
            {
                return null;
            }

            return new Address(country, street1, street2, city, state, zipCode);
    }

}