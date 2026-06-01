using System.Reflection;
using System.Text;

namespace MPL.X4.Services;

/// <summary>
/// A class that provides helper functions for debug output.
/// </summary>
internal static class DebugOutputHelper
{
    private const BindingFlags PublicPropertyFlags = BindingFlags.Public | BindingFlags.Instance;

    /// <summary>
    /// Gets a string containing the name of all properties of type <typeparamref name="T"/> from the specified <paramref name="instance"/> that match <paramref name="valueEvaluator"/>.
    /// </summary>
    /// <typeparam name="T">The type of the property, which must be a value type.</typeparam>
    /// <param name="instance">An <see cref="object"/> that is the instance to evaluate.</param>
    /// <param name="valueEvaluator">A <see cref="Func{T, TResult}"/> that is the value evaluator to use.</param>
    /// <returns>A <see cref="string"/> containing the result.</returns>
    internal static string GetPropertyValues<T>(object instance, Func<T, bool> valueEvaluator)
        where T : struct
    {
        var returnValue = new StringBuilder();
        var tType = typeof(T);

        var properties = instance
                                 .GetType()
                                 .GetProperties(PublicPropertyFlags);

        foreach (var property in properties)
        {
            if (property.CanRead &&
                property.PropertyType == tType)
            {
                var value = property.GetValue(instance);

                if (value is T tValue &&
                    valueEvaluator(tValue))
                {
                    returnValue.Append($"{property.Name}: {tValue}, ");
                }
            }
        }

        return returnValue.ToString();
    }

}
