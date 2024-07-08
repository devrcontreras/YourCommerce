using FluentValidation;

namespace YourCommerce.Application.Customers.Delete;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{

    public DeleteCustomerCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}