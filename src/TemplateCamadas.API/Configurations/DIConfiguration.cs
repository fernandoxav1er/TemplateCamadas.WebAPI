namespace TemplateCamadas.API.Configurations;

public static class DIConfiguration
{
    public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
    {
        // Configuration
        services.AddHttpContextAccessor();
        //services.AddScoped<INotificationService, NotificationService>();
        //services.AddScoped<ISqlExecutorRepository, SqlExecutorRepository>();

        // Services

        // Repositories

        return services;
    }
}
