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
        app.UseCors("CorsPolicy");

        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseEndpoints(options =>
        {
            options.UseHealthCheckCustom();
            options.MapControllers();
        });

        return app;
    }
}
