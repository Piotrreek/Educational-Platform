using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Application.Extensions;

public static class ExerciseSolutionExtensions
{
    public static IEnumerable<ExerciseSolutionReviewDto> GetReviews(this ExerciseSolution solution)
    {
        return solution.Reviews
            .Select(review =>
                new ExerciseSolutionReviewDto(review.Id, review.Content, review.Author.UserName,
                    review.CreatedOn.DateTime, review.FileName != null));
    }
}