using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Results;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.Abstractions;

public abstract class RatingHandler<TEntity, TRating>
    where TRating : RatingEntity, IRatingEntity<TRating> where TEntity : EntityWithRatings<TRating>
{
    private readonly IUserRepository _userRepository;

    protected RatingHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected async Task<OneOf<Success<RatingDto>, BadRequestResult>> AddRating(TEntity entityWithRatings, Guid userId,
        decimal rating)
    {
        var userResult = await _userRepository.GetUserByIdAsync(userId);

        if (userResult.IsT1)
        {
            return new BadRequestResult(UserErrorMessages.UserWithIdNotExists);
        }

        var result = entityWithRatings.AddNewRating(rating, userId);

        if (!result.TryPickT0(out var success, out var badRequest))
        {
            return badRequest;
        }

        return new Success<RatingDto>(new RatingDto(success.Value, entityWithRatings.GetLastRatings(5)));
    }

    protected async Task<OneOf<Success<RatingDto>, BadRequestResult>> RemoveRating(TEntity entityWithRatings,
        Guid userId)
    {
        var userResult = await _userRepository.GetUserByIdAsync(userId);
        if (userResult.IsT1)
            return new BadRequestResult(UserErrorMessages.UserWithIdNotExists);

        var averageRating = entityWithRatings.RemoveRatingForUser(userId);

        return new Success<RatingDto>(
            new RatingDto(averageRating, entityWithRatings.GetLastRatings(5)));
    }
}