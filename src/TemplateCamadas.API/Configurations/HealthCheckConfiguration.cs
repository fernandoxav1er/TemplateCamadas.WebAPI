using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace TemplateCamadas.API.Configurations;

public static class HealthCheckConfig
{
    public static IServiceCollection AddHealthCheckConfiguration(this IServiceCollection services, IConfiguration configuration, int maxAllocatedMemoryMb = 1024)
    {
        services.AddHealthChecks()
                .AddProcessAllocatedMemoryHealthCheck(maxAllocatedMemoryMb, tags: new string[] { "Memory in use", $"Max of: {maxAllocatedMemoryMb}" });

        services.AddHealthChecksUI(setupSettings: setup => { }).AddInMemoryStorage();

        return services;
    }

    public static IEndpointRouteBuilder UseHealthCheckCustom(this IEndpointRouteBuilder app)
    {
        app.MapHealthChecks("/healthchecks", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapHealthChecksUI(options =>
        {
            options.UIPath = "/monitor";
            options.AddCustomStylesheet(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "healthcheck", "HealthCheckStyle.css"));
        });

        return app;
    }
}
