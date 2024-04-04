using ErrorOr;
using MediatR;
using YourCommerce.Domain.Primitives;

namespace YourCommerce.Application.Customers.Create;

public record CreateCustomerCommand(
    string Name,
    string LastName,
    string Email,
    string PhoneNumber,
    string Country,
    string Street1,
    string Street2,
    string City,
    string State,
    string ZipCode) : IRequest<ErrorOr<Unit>>;