﻿using ErrorOr;
using FluentValidation;
using MediatR;

namespace YourCommerce.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehaviour(IValidator<TRequest> validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(_validator is null)
        {
            return await next();
        }

        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if(validatorResult.IsValid)
        {
             return await next();
        }

        var errors = validatorResult.Errors
            .ConvertAll(e => Error.Validation(e.PropertyName, e.ErrorMessage)); 

        return (dynamic)errors;
        
    }
}
