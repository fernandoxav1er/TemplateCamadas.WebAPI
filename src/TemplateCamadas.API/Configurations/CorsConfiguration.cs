namespace TemplateCamadas.API.Configurations;

public static class CorsConfiguration
{
    private const string PolicyName = "CorsPolicy";

    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
        var allowAnyOrigin = allowedOrigins.Contains("*");

        services.AddCors(options =>
        {
            options.AddPolicy(PolicyName, policy =>
            {
                if (allowAnyOrigin)
                    policy.AllowAnyOrigin();
                else
                    policy.WithOrigins(allowedOrigins);

                policy.AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        return services;
    }

    public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder app)
    {
        return app.UseCors(PolicyName);
    }
}
