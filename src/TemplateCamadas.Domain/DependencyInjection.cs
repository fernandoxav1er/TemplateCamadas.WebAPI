using Microsoft.Extensions.DependencyInjection;
using TemplateCamadas.Domain.Interfaces;
using TemplateCamadas.Domain.Services;

namespace TemplateCamadas.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
}
