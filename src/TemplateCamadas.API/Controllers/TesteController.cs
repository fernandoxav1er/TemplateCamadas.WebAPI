using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TemplateCamadas.Domain.Interfaces;

namespace TemplateCamadas.API.Controllers;

[Route("v1/teste")]
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
    public async Task<IActionResult> Get()
    {
        await Task.Delay(2000); 
        var result = "Deu bom!";
        return CustomResponse(result);
    }
}
