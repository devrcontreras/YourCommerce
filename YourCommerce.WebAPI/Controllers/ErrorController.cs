using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace YourCommerce.WebAPI.Controllers;

public class ErrosController : ControllerBase
{

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult Error()
    {
        
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem();

    }
}