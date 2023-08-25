using EducationalPlatform.Application.Contracts.Academy.AcademyEntityRequest;
using EducationalPlatform.Application.Helpers;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Results;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Academy.AcademyEntityRequest.ResolveCreateAcademyEntityRequest;

public abstract class ResolveCreateAcademyEntityRequestHandler
{
    protected abstract Func<Domain.Entities.CreateAcademyEntityRequest, OneOf<Success, BadRequestResult>> ResolveRequest
    {
        get;
    }

    private readonly IAcademyRepository _academyRepository;

    protected ResolveCreateAcademyEntityRequestHandler(IAcademyRepository academyRepository)
    {
        _academyRepository = academyRepository;
    }

    protected async Task<OneOf<Success<IEnumerable<GroupedCreateAcademyEntityRequestDto>>, NotFound, BadRequestResult>>
        Resolve(Guid id)
    {
        var requestResult = await _academyRepository.GetRequestByIdAsync(id);

        if (!requestResult.TryPickT0(out var createEntityRequest, out var notFound))
        {
            return notFound;
        }

        var acceptResult = ResolveRequest(createEntityRequest);

        if (acceptResult.TryPickT1(out var badRequestResult, out _))
        {
            return badRequestResult;
        }

        var requests = (await _academyRepository.GetNotResolvedRequestsAsync()).ToList();

        requests.Remove(createEntityRequest);

        return new Success<IEnumerable<GroupedCreateAcademyEntityRequestDto>>(
            requests.GetGroupedCreateAcademyEntityRequests());
    }
}