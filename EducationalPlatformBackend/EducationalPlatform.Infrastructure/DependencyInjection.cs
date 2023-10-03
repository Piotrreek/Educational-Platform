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

        services.AddScoped<IRoleRepository, RoleRepository>(provider =>
        {
            var dbContext = provider.GetRequiredService<EducationalPlatformDbContext>();

            return new RoleRepository(dbContext.Roles);
        });

        services.AddScoped<IUserRepository, UserRepository>(provider =>
        {
            var dbContext = provider.GetRequiredService<EducationalPlatformDbContext>();

            return new UserRepository(dbContext.Users);
        });

        services.AddScoped<IAcademyRepository, AcademyRepository>(provider =>
        {
            var dbContext = provider.GetRequiredService<EducationalPlatformDbContext>();

            return new AcademyRepository(dbContext.Universities, dbContext.UniversityCourses,
                dbContext.UniversitySubjects, dbContext.Faculties, dbContext.CreateAcademyEntityRequests);
        });

        services.AddScoped<IDidacticMaterialRepository, DidacticMaterialRepository>(provider =>
        {
            var dbContext = provider.GetRequiredService<EducationalPlatformDbContext>();

            return new DidacticMaterialRepository(dbContext.DidacticMaterials);
        });

        services.AddScoped<IExerciseRepository, ExerciseRepository>(provider =>
        {
            var dbContext = provider.GetRequiredService<EducationalPlatformDbContext>();

            return new ExerciseRepository(dbContext.Exercises, dbContext.ExerciseSolutions,
                dbContext.ExerciseSolutionReviews);
        });

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