using EducationalPlatform.Domain.Abstractions;
using EducationPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPlatform.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<EducationalPlatformDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EducationalPlatformDb"),
                b => b.MigrationsAssembly(typeof(EducationalPlatformDbContext).Assembly.FullName));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}