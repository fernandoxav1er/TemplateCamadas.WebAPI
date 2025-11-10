using Microsoft.AspNetCore.Mvc;
using TemplateCamadas.Domain.Interfaces;
using TemplateCamadas.Domain.Models.Responses;

namespace TemplateCamadas.API.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public MainController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    protected IActionResult CustomResponse<TData>(TData data)
    {
        if (_notificationService.HasNotifications())
        {
            return BadRequest(GenerateBadRequestResponse());
        }

        if (data is null) return NotFound();

        return Ok(new ResponseBase<TData> { Data = data });
    }

    protected IActionResult CustomResponse()
    {
        if (_notificationService.HasNotifications())
        {
            return BadRequest(GenerateBadRequestResponse());
        }

        return Ok(new ResponseBase());
    }

    private object GenerateBadRequestResponse()
    {
        return new ResponseBase
        {
            Result = new Result
            {
                Success = false,
                Messages = _notificationService.GetNotifications()
            }
        };
    }
}
