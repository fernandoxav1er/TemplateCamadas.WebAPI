using Microsoft.Extensions.DependencyInjection;
using TemplateCamadas.Application.UseCases.Samples;

namespace TemplateCamadas.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateSampleUseCase>();

        return services;
    }
}
