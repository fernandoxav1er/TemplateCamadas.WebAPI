using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TemplateCamadas.Application.Dtos.Requests;
using TemplateCamadas.Application.UseCases.Samples;
using TemplateCamadas.Domain.Interfaces;

namespace TemplateCamadas.API.Controllers;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/samples")]
public class SampleController : MainController
{
    private readonly CreateSampleUseCase _createSampleUseCase;

    public SampleController(INotificationService notificationService, CreateSampleUseCase createSampleUseCase)
        : base(notificationService)
    {
        _createSampleUseCase = createSampleUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSampleRequest request)
    {
        var result = await _createSampleUseCase.Process(request);
        return CustomResponse(result);
    }
}
