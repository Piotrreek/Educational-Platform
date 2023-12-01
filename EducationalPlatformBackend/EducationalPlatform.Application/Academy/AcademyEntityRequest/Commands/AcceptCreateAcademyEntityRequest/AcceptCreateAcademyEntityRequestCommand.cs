using EducationalPlatform.Application.Contracts.Academy.AcademyEntityRequest;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.AcademyEntityRequest.Commands.AcceptCreateAcademyEntityRequest;

public record AcceptCreateAcademyEntityRequestCommand(Guid Id)
    : IRequest<OneOf<Success<IEnumerable<GroupedCreateAcademyEntityRequestDto>>, NotFound, BadRequestResult>>;