using Microsoft.EntityFrameworkCore;

namespace TemplateCamadas.API.Configurations;

public static class DataSeeder
{
    public static IApplicationBuilder UseDataSeeder<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
    {
        using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
        TDbContext requiredService = serviceScope.ServiceProvider.GetRequiredService<TDbContext>();
        requiredService.Database.Migrate();
        return app;
    }

}
