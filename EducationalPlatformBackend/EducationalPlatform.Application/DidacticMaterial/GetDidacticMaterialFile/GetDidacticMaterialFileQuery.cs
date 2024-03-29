using EducationalPlatform.Application.Models;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.DidacticMaterial.GetDidacticMaterialFile;

public record GetDidacticMaterialFileQuery(Guid DidacticMaterialId) : IRequest<OneOf<NotFound, BlobDto>>;