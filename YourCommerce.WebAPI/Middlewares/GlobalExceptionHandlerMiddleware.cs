using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace YourCommerce.WebAPI.Middlewares;

public class GlobalExceptionHandlerMiddlerMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddlerMiddleware> _logger;
    
    public GlobalExceptionHandlerMiddlerMiddleware(ILogger<GlobalExceptionHandlerMiddlerMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = "An internal server error ocurred.", 
            };

            var json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}