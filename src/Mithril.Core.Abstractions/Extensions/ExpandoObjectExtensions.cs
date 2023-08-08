using BigBook;
using System.Dynamic;
using System.Reflection;

namespace Mithril.Core.Abstractions.Extensions
{
    /// <summary>
    /// ExpandoObject extension methods
    /// </summary>
    public static class ExpandoObjectExtensions
    {
        /// <summary>
        /// Converts the ExpandoObject to the type specified.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The view model equivalent.</returns>
        public static TEntity? ConvertExpando<TEntity>(this ExpandoObject? value)
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

        /// <summary>
        /// Converts the object to an expando.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The resulting object</returns>
        public static ExpandoObject? ConvertToExpando<TEntity>(this TEntity? value)
        {
            if (value is null)
                return null;
            var ReturnValue = new ExpandoObject();
            var ReturnValueDictionary = ReturnValue as IDictionary<string, object?>;

            foreach (PropertyInfo Property in value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                ReturnValueDictionary[Property.Name.ToString(StringCase.CamelCase)] = Property.GetValue(value);
            }

            return ReturnValue;
        }
    }
}