using EducationalPlatform.Domain.Abstractions;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Abstractions.Services;
using EducationPlatform.Infrastructure.Persistence;
using EducationPlatform.Infrastructure.Persistence.Repositories;
using EducationPlatform.Infrastructure.Services;
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
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUserContextService, UserContextService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IEmailService, DevNotificationsSender>();

        return services;
    }
}