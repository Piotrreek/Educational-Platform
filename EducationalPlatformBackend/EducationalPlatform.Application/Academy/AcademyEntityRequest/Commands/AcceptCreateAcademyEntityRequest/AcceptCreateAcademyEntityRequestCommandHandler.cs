using EducationalPlatform.Application.Academy.AcademyEntityRequest.Commands.ResolveCreateAcademyEntityRequest;
using EducationalPlatform.Application.Contracts.Academy.AcademyEntityRequest;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.AcademyEntityRequest.Commands.AcceptCreateAcademyEntityRequest;

internal sealed class AcceptCreateAcademyEntityRequestCommandHandler : ResolveCreateAcademyEntityRequestHandler,
    IRequestHandler<AcceptCreateAcademyEntityRequestCommand,
        OneOf<Success<IEnumerable<GroupedCreateAcademyEntityRequestDto>>, NotFound, BadRequestResult>>
{
    protected override Func<Domain.Entities.CreateAcademyEntityRequest, OneOf<Success, BadRequestResult>>
        ResolveRequest =>
        request => request.Accept();

    public AcceptCreateAcademyEntityRequestCommandHandler(IAcademyRepository academyRepository) : base(
        academyRepository)
    {
    }

    public async Task<OneOf<Success<IEnumerable<GroupedCreateAcademyEntityRequestDto>>, NotFound, BadRequestResult>>
        Handle(AcceptCreateAcademyEntityRequestCommand request, CancellationToken cancellationToken)
    {
        return await Resolve(request.Id);
    }
}