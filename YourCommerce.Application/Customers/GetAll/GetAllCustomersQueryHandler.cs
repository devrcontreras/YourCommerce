using ErrorOr;
using MediatR;
using YourCommerce.Application.Customers.Common;
using YourCommerce.Domain.Customers;
using YourCommerce.Domain.Redis;

namespace YourCommerce.Application.Customers.GetAll;

internal sealed class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, ErrorOr<IReadOnlyList<CustomerResponse>>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IRedisRepository _redisRepository;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IRedisRepository redisRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _redisRepository = redisRepository;
    }

    public async Task<ErrorOr<IReadOnlyList<CustomerResponse>>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
    {
        const string KEY = "ALL_CUSTOMERS";

        var values = await _redisRepository.GetCacheAsync<ErrorOr<IReadOnlyList<CustomerResponse>>>(KEY);

        if(!values.IsError)
        {
            return values;
        }

        IReadOnlyList<Customer> customers = await _customerRepository.GetAll();

        var customersResponse = customers.Select(customer => new CustomerResponse(
                customer.Id.Value,
                customer.FullName,
                customer.Email,
                customer.PhoneNumber.Value,
                new AddressResponse(customer.Address.Country,
                    customer.Address.Street1,
                    customer.Address.Street2,
                    customer.Address.City,
                    customer.Address.State,
                    customer.Address.ZipCode),
                    customer.Active
            )).ToList();

        await _redisRepository.SaveCacheAsync<ErrorOr<IReadOnlyList<CustomerResponse>>>(KEY, customersResponse);

        return customersResponse;

    }
}