using TemplateCamadas.API.Configurations;
using TemplateCamadas.Application;
using TemplateCamadas.Domain;
using TemplateCamadas.Infrastructure;

namespace TemplateCamadas.API;

public class Startup
{
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Env { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddExceptionHandlerConfiguration();

        services.AddDomain();
        services.AddInfrastructure(Configuration);
        services.AddApplication();

        services.AddWebApiConfiguration(Configuration);
        services.AddApiVersioningConfiguration();
        services.AddSwaggerConfiguration(Configuration);
        services.AddCorsConfiguration(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseWebApiConfiguration(Configuration, env);
    }
}
