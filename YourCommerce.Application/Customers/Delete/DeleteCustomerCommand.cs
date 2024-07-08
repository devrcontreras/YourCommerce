using ErrorOr;
using MediatR;

namespace YourCommerce.Application.Customers.Delete;

public record DeleteCustomerCommand(Guid Id): IRequest<ErrorOr<Unit>>;