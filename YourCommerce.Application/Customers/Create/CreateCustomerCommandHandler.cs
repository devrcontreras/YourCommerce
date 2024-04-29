using ErrorOr;
using MediatR;
using YourCommerce.Domain.Customers;
using YourCommerce.Domain.Primitives;
using YourCommerce.Domain.ValueObjects;

namespace YourCommerce.Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Unit>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;


    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
    }
    
    public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {

            if(PhoneNumber.Create(request.PhoneNumber) is not PhoneNumber phoneNumber)
            {
                return Error.Validation("Customer.PhoneNumber", "Phone number has not format.");
            }

            if(Address.Create(request.Country, request.Street1, request.Street2, request.City, request.State, request.ZipCode)
                is not Address address)
            {
                return Error.Validation("Customer.ddress", "Address is not valid.");
            }

            var customer = new Customer(
                new CustomerId(Guid.NewGuid()),
                request.Name,
                request.LastName,
                request.Email,
                phoneNumber,
                address,
                true
                );

                await _customerRepository.Add(customer);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
    }
}