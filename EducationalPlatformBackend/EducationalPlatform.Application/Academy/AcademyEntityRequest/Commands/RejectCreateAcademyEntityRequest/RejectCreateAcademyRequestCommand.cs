using EducationalPlatform.Application.Contracts.Academy.AcademyEntityRequest;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.AcademyEntityRequest.Commands.RejectCreateAcademyEntityRequest;

public record RejectCreateAcademyRequestCommand(Guid Id) : IRequest<OneOf<Success<IEnumerable<GroupedCreateAcademyEntityRequestDto>>, NotFound, BadRequestResult>>;