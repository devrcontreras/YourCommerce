using ErrorOr;
using MediatR;
using YourCommerce.Application.Customers.Common;

namespace YourCommerce.Application.Customers.GetAll;

public record GetAllCustomersQuery() : IRequest<ErrorOr<IReadOnlyList<CustomerResponse>>>;