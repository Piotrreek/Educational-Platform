using Azure.Storage.Blobs;
using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Configuration;
using EducationalPlatform.Domain.Abstractions;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationPlatform.Infrastructure.Persistence;
using EducationPlatform.Infrastructure.Persistence.Repositories;
using EducationPlatform.Infrastructure.Services;
using EducationPlatform.Infrastructure.Services.Cache;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace EducationPlatform.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        #region Database

        services.AddDbContext<EducationalPlatformDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EducationalPlatformDb"),
                b => b.MigrationsAssembly(typeof(EducationalPlatformDbContext).Assembly.FullName));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #endregion

        #region Repositories

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

        #endregion

        #region AuthenticationServices

        services.AddScoped<IUserContextService, UserContextService>();
        services.AddScoped<IJwtService, JwtService>();

        #endregion

        #region ExternalServices

        services.AddScoped<IAzureBlobStorageService>(
            provider =>
                new CacheAzureBlobStorageService(
                    new AzureBlobStorageService(
                        provider.GetRequiredService<BlobServiceClient>(),
                        provider.GetRequiredService<IOptions<AzureBlobStorageConfiguration>>()
                    ),
                    provider.GetRequiredService<IMemoryCache>()
                )
        );

        services.AddScoped(_ => new BlobServiceClient(configuration.GetValue<string>("AzureBlobStorage:Container")));
        if (environment.IsDevelopment())
        {
            services.AddScoped<IEmailService, DevNotificationsSender>();
        }
        else
        {
            services.AddScoped<IEmailService, EmailService>();
        }

        #endregion

        return services;
    }
}