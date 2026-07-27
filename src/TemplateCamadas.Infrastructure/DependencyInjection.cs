using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateCamadas.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<DatabaseContext>(options =>
        //    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))); // requires: using Microsoft.EntityFrameworkCore;

        //services.AddScoped<ISqlExecutorRepository, SqlExecutorRepository>();
        //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }
}
