using EducationalPlatform.Application.Academy.AcademyEntityRequest.ResolveCreateAcademyEntityRequest;
using EducationalPlatform.Application.Contracts.Academy.AcademyEntityRequest;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf.Types;
using OneOf;

namespace EducationalPlatform.Application.Academy.AcademyEntityRequest.RejectCreateAcademyEntityRequest;

internal sealed class RejectCreateAcademyRequestCommandHandler : ResolveCreateAcademyEntityRequestHandler,
    IRequestHandler<RejectCreateAcademyRequestCommand,
        OneOf<Success<IEnumerable<GroupedCreateAcademyEntityRequestDto>>, NotFound, BadRequestResult>>
{
    protected override Func<Domain.Entities.CreateAcademyEntityRequest, OneOf<Success, BadRequestResult>> ResolveRequest =>
        request => request.Reject();

    public RejectCreateAcademyRequestCommandHandler(IAcademyRepository academyRepository) : base(academyRepository)
    {
    }

    public async Task<OneOf<Success<IEnumerable<GroupedCreateAcademyEntityRequestDto>>, NotFound, BadRequestResult>>
        Handle(RejectCreateAcademyRequestCommand request, CancellationToken cancellationToken)
    {
        return await Resolve(request.Id);
    }
}