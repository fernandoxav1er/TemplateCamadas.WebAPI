using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TemplateCamadas.Domain.Interfaces;

namespace TemplateCamadas.API.Controllers;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/teste")]
public class TesteController : MainController
{
    public TesteController(INotificationService notificationService) : base(notificationService) 
    {
    }

    [HttpGet]
    [SwaggerOperation(
        summary: "Texto teste",
        description: "Descrição teste"
    )]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Get()
    {
        var result = "Deu bom!";
        return CustomResponse(result);
    }
}
