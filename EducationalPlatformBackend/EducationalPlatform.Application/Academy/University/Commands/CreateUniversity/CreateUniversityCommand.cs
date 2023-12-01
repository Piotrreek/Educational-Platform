using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.University.Commands.CreateUniversity;

public record CreateUniversityCommand(string UniversityName) : IRequest<OneOf<Success, BadRequestResult>>;