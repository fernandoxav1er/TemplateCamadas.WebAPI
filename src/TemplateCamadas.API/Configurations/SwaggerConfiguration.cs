using Swashbuckle.AspNetCore.SwaggerGen;

namespace TemplateCamadas.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.ConfigureOptions<ConfigureSwaggerOptions>();

            services.AddSwaggerGen(options => options.EnableAnnotations());

            return services;
        }
    }
}
