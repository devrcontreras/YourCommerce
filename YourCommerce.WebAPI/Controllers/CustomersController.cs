using MediatR;
using Microsoft.AspNetCore.Mvc;
using YourCommerce.Application.Customers.Create;
using YourCommerce.Domain.Customers;

namespace YourCommerce.WebAPI.Controllers;

[Route("[Controller]")]
public class CustomersController : ApiController
{
    private readonly ISender _mediator;

    public CustomersController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var createCustomerResult = await _mediator.Send(command);

        return createCustomerResult.Match(
            customer => Ok(),
            errors => Problem(errors)
        );
    }

}