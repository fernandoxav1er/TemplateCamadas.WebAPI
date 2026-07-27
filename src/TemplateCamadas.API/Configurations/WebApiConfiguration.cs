using Asp.Versioning.ApiExplorer;

namespace TemplateCamadas.API.Configurations;

public static class WebApiConfiguration
{
    public static IServiceCollection AddWebApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddHealthCheckConfiguration(configuration);

        return services;
    }

    public static IApplicationBuilder UseWebApiConfiguration(this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment env)
    {
        //app.UseDataSeeder<DatabaseContext>();
        app.UseExceptionHandlerConfiguration();
        app.UseCorsConfiguration();

        app.UseHttpsRedirection();
        app.UseRouting();

        if (!env.IsProduction())
        {
            app.UseSwagger();

            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }

        app.UseEndpoints(options =>
        {
            options.UseHealthCheckCustom();
            options.MapControllers();
        });

        return app;
    }
}
