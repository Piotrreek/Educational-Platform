using Azure.Storage.Blobs;
using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Domain.Abstractions;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationPlatform.Infrastructure.Persistence;
using EducationPlatform.Infrastructure.Persistence.Repositories;
using EducationPlatform.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EducationPlatform.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddDbContext<EducationalPlatformDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EducationalPlatformDb"),
                b => b.MigrationsAssembly(typeof(EducationalPlatformDbContext).Assembly.FullName));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAcademyRepository, AcademyRepository>();
        services.AddScoped<IGeneralRepository, GeneralRepository>();
        services.AddScoped<IDidacticMaterialRepository, DidacticMaterialRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();

        services.AddScoped<IUserContextService, UserContextService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
        services.AddScoped(x => new BlobServiceClient(configuration.GetValue<string>("AzureBlobStorage:Container")));
        if (environment.IsDevelopment())
        {
            services.AddScoped<IEmailService, DevNotificationsSender>();
        }

        if (environment.IsProduction())
        {
            services.AddScoped<IEmailService, EmailService>();
        }

        return services;
    }
}