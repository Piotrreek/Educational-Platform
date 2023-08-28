using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Primitives;

public abstract class EntityWithRatings<T> : Entity where T : RatingEntity, IRatingEntity<T>
{
    private readonly List<T> _ratings = new();
    public IReadOnlyCollection<T> Ratings => _ratings;
    public decimal AverageRating => Ratings.Count > 0 ? Ratings.Sum(s => s.Rating) / Ratings.Count : 0;

    public OneOf<Success<decimal>, BadRequestResult> AddNewRating(decimal rating, Guid userId)
    {
        if (rating is < (decimal)0.5 or > 5)
        {
            return new BadRequestResult(DidacticMaterialErrorMessages.BadRatingValue);
        }

        if (_ratings.Any(r => r.UserId == userId))
        {
            return new BadRequestResult(DidacticMaterialErrorMessages.CannotSetRatingTwiceByOneUser);
        }

        _ratings.Add(T.Create(rating, userId, Id));

        return new Success<decimal>(AverageRating);
    }

    public decimal RemoveRatingForUser(Guid userId)
    {
        if (TryGetDidacticMaterialRating(userId, out var rating))
            _ratings.Remove(rating!);

        return AverageRating;
    }

    public bool TryGetDidacticMaterialRating(Guid? userId, out T? rating)
    {
        rating = _ratings.SingleOrDefault(r => r.UserId == userId);

        return rating is not null;
    }

    public IEnumerable<decimal> GetLastRatings(int count)
    {
        return _ratings.OrderByDescending(c => c.CreatedOn).Take(count).Select(c => c.Rating);
    }
}