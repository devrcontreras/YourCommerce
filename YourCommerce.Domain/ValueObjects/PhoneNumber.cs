using System.Text.RegularExpressions;

namespace YourCommerce.Domain.ValueObjects;

public partial record PhoneNumber
{

    private const int DefaultLength = 12;

    private const string Pattern = @"^(?:-*\d-*){10}$";

    [GeneratedRegex(Pattern)]
    private static partial Regex PhoneNumberRegex();

    private PhoneNumber(string value) => Value = value;

    public static PhoneNumber? Create(string value)
    {
        if(string.IsNullOrEmpty(value) || !PhoneNumberRegex().IsMatch(value) || value.Length != DefaultLength)
        {
            return null;
        }

        return new PhoneNumber(value);
    }

    public string Value { get; set; }
   
}