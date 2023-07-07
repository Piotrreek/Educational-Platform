using System.Linq.Expressions;
using System.Reflection;

namespace EducationalPlatform.Domain.Tests.Helpers;

public class SetPropertyHelpers
{
    public static void SetProperty<TRequest, TProperty>(TRequest obj, Expression<Func<TRequest, TProperty>> property,
        TProperty value)
    {
        var propertyInfo = (PropertyInfo)((MemberExpression)property.Body).Member;
        propertyInfo.SetValue(obj, value);
    }

    public static void SetProperty<T>(T obj, string propertyName, object value) where T : class
    {
        var propertyInfo = typeof(T).GetField(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);
        propertyInfo!.SetValue(obj, value);
    }
}