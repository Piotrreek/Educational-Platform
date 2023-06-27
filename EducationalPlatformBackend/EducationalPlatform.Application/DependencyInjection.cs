using EducationalPlatform.Application.Behaviors;
using EducationalPlatform.Application.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EducationalPlatform.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            config.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
        });

        services.AddValidatorsFromAssemblyContaining(typeof(ApplicationAssemblyReference));

        return services;
    }
}