using System.Diagnostics;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using YourCommerce.WebAPI.Common.Http;

namespace YourCommerce.WebAPI.Common.Errors;

public class YourCommerceProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;

    public YourCommerceProblemDetailsFactory(ApiBehaviorOptions options)
    {
        _options = options ?? throw new ArgumentException(nameof(options));
    }

    public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
    {

        statusCode ??= 500;

        var problemDetails = new ProblemDetails{
            Status = statusCode,
            Title = title,
            Type  = type,
            Detail = detail,
            Instance = instance
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;

    }

    public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
    {
        if(modelStateDictionary == null)
        {
            throw new ArgumentNullException(nameof(modelStateDictionary));
        }

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance
        };

        if(title != null)
        {
            problemDetails.Title = title;
        }

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }
    

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {

            problemDetails.Status ??= statusCode;

            if(_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorddata))
            {
                problemDetails.Title ??= clientErrorddata.Title;
                problemDetails.Type ??= clientErrorddata.Link;
            }

            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            if(traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

            var errors = httpContext?.Items[HttpContextItemKeys.Error] as List<Error>;

            if(errors is not null)
            {
                problemDetails.Extensions.Add("errorCodes", errors.Select(e=> e.Code));
            }

    }
}