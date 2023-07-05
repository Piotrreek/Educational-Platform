using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf.Types;
using OneOf;

namespace EducationalPlatform.Application.Academy.University.CreateUniversity;

public record CreateUniversityCommand(string UniversityName) : IRequest<OneOf<Success, BadRequestResult>>;