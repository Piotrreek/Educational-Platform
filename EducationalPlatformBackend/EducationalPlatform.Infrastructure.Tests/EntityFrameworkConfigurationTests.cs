using System.Reflection;
using EducationalPlatform.Domain.Primitives;
using EducationPlatform.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EducationalPlatform.Infrastructure.Tests;

public class EntityFrameworkConfigurationTests
{
    [Fact]
    public void EveryEntity_ShouldHaveEFConfigurationClassWithValueGeneratedNeverForId()
    {
        var entityTypes = Assembly.GetAssembly(typeof(Entity))!
            .GetTypes()
            .Where(t => t.IsClass && t.IsSubclassOf(typeof(Entity)) && !t.IsGenericType && !t.IsAbstract)
            .ToList();

        var entityTypeConfigurations = Assembly.GetAssembly(typeof(EducationalPlatformDbContext))!
            .GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType &&
                          i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
            .ToList();

        var entityTypesFromConfigurations = entityTypeConfigurations.Select(entityTypeConfiguration =>
                entityTypeConfiguration.GetInterfaces()
                    .Single(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    .GetGenericArguments()
                    .Single())
            .ToList();

        var groupedEntityTypesFromConfiguration = entityTypesFromConfigurations.GroupBy(e => e.Name);

        groupedEntityTypesFromConfiguration.Count().Should().Be(entityTypes.Count);

        var builder = new DbContextOptionsBuilder();
        builder.UseInMemoryDatabase(databaseName: "test");
        var dbContext =
            new EducationalPlatformDbContext(builder.Options);

        foreach (var entityType in entityTypes)
        {
            if (entityType.GetProperty("Id") != null)
            {
                var idConfiguration = dbContext.Model.FindEntityType(entityType)!.FindProperty("Id")!
                    .ValueGenerated;

                idConfiguration.Should().Be(ValueGenerated.Never);
            }
        }
    }
}