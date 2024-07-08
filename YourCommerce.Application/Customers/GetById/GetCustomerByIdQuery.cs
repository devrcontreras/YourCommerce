using ErrorOr;
using MediatR;
using YourCommerce.Application.Customers.Common;

namespace YourCommerce.Application.Customers.GetById;

public record GetCustomerByIdQuery(Guid Id) : IRequest<ErrorOr<CustomerResponse>>;