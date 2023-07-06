using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Domain.Extensions;

public static class OneOfExtensions
{
    public static OneOf<T, NotFound> GetValueOrNotFoundResult<T>(T? obj) where T : class
    {
        return obj == null ? new NotFound() : obj;
    }
}