using EducationalPlatform.Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterial;

public record CreateDidacticMaterialCommand(string Name, string DidacticMaterialType,
    Guid UniversityCourseId, Guid AuthorId, string[]? KeyWords, string? Description,
    string? Content, IFormFile? File) : IRequest<OneOf<Success, BadRequestResult, ServiceUnavailableResult>>;