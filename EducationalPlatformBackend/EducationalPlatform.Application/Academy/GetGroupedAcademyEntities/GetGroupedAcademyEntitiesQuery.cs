using EducationalPlatform.Application.Contracts.Academy.University;
using MediatR;

namespace EducationalPlatform.Application.Academy.GetGroupedAcademyEntities;

public record GetGroupedAcademyEntitiesQuery : IRequest<IEnumerable<UniversityDto>>;