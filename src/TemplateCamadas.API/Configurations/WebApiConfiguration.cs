namespace TemplateCamadas.API.Configurations;

public static class WebApiConfiguration
{
    public static IServiceCollection AddWebApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddHttpClient();

        return services;
    }

    public static IApplicationBuilder UseWebApiConfiguration(this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment env)
    {
        //app.UseDataSeeder<DatabaseContext>();
        app.UseCors("CorsPolicy");
        app.UseRouting();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseEndpoints(options =>
        {
            options.MapControllers();
        });


        return app;
    }
}
