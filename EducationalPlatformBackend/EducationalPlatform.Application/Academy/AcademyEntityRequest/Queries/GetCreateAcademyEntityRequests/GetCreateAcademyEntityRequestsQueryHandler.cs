using EducationalPlatform.Application.Contracts.Academy.AcademyEntityRequest;
using EducationalPlatform.Application.Helpers;
using EducationalPlatform.Domain.Abstractions.Repositories;
using MediatR;

namespace EducationalPlatform.Application.Academy.AcademyEntityRequest.Queries.GetCreateAcademyEntityRequests;

internal sealed class GetCreateAcademyEntityRequestsQueryHandler : IRequestHandler<GetCreateAcademyEntityRequestsQuery,
    IEnumerable<GroupedCreateAcademyEntityRequestDto>>
{
    private readonly IAcademyRepository _academyRepository;

    public GetCreateAcademyEntityRequestsQueryHandler(IAcademyRepository academyRepository)
    {
        _academyRepository = academyRepository;
    }

    public async Task<IEnumerable<GroupedCreateAcademyEntityRequestDto>> Handle(
        GetCreateAcademyEntityRequestsQuery request,
        CancellationToken cancellationToken)
    {
        var requests = await _academyRepository.GetNotResolvedRequestsAsync();

        return requests.GetGroupedCreateAcademyEntityRequests();
    }
}