using TemplateCamadas.Domain.Interfaces;
using TemplateCamadas.Domain.Services;
using TemplateCamadas.Infrastructure.Repositories;

namespace TemplateCamadas.API.Configurations;

public static class DIConfiguration
{
    public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<INotificationService, NotificationService>();
        //services.AddScoped<ISqlExecutorRepository, SqlExecutorRepository>();
        //services.AddScoped<IProcedureExecutorRepository, ProcedureExecutorRepository>();

        return services;
    }
}
