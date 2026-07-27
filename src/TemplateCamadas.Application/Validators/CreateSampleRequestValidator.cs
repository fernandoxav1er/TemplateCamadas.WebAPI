using FluentValidation;
using TemplateCamadas.Application.Dtos.Requests;

namespace TemplateCamadas.Application.Validators;

public class CreateSampleRequestValidator : AbstractValidator<CreateSampleRequest>
{
    public CreateSampleRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
