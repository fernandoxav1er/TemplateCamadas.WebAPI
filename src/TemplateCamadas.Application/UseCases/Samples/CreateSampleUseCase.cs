using TemplateCamadas.Application.Dtos.Requests;
using TemplateCamadas.Application.Dtos.Responses;
using TemplateCamadas.Application.Mappings;
using TemplateCamadas.Application.Validators;
using TemplateCamadas.Domain.Interfaces;

namespace TemplateCamadas.Application.UseCases.Samples;

public class CreateSampleUseCase : BaseUseCase<SampleResponse?, CreateSampleRequest>
{
    public CreateSampleUseCase(INotificationService notificationService) : base(notificationService) { }

    public override Task<SampleResponse?> Process(CreateSampleRequest parameters)
    {
        if (!ExecuteValidations(new CreateSampleRequestValidator(), parameters))
            return Task.FromResult<SampleResponse?>(null);

        var response = parameters.ToEntity().ToResponse();

        return Task.FromResult<SampleResponse?>(response);
    }
}
