using TemplateCamadas.API.Configurations;

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
        //services.AddDbContext<DatabaseContext>(options =>
        //    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))); // requires: using Microsoft.EntityFrameworkCore;

        services.AddDependencyInjectionConfiguration();
        services.AddWebApiConfiguration(Configuration);
        services.AddSwaggerConfiguration(Configuration);

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseWebApiConfiguration(Configuration, env);
    }
}
