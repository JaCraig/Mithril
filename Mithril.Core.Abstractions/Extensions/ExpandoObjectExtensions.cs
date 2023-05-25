using BigBook;
using System.Dynamic;

namespace Mithril.Core.Abstractions.Extensions
{
    /// <summary>
    /// ExpandoObject extension methods
    /// TODO: Add tests
    /// </summary>
    public static class ExpandoObjectExtensions
    {
        /// <summary>
        /// Converts the ExpandoObject to the type specified.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The view model equivalent.
        /// </returns>
        public static TEntity? ConvertExpando<TEntity>(this ExpandoObject value)
        {
            if (value is null)
                return default;
            IDictionary<string, object?> Values = value;
            var TempValues = new Dictionary<string, object?>();
            foreach (var Key in Values.Keys)
            {
                TempValues.Add(Key.ToPascalCase(), Values[Key]);
            }
            dynamic TempValue = new Dynamo(TempValues);
            return (TEntity)TempValue;
        }
    }
}