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
                var Value = Values[Key];
                TempValues.Add(Key.ToPascalCase(), Value);
            }
            dynamic TempValue = new Dynamo(TempValues);
            return (TEntity)TempValue;
        }

        /// <summary>
        /// Converts the object to an expando.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The resulting object
        /// </returns>
        public static ExpandoObject? ConvertToExpando(this object? value)
        {
            if (value is null)
                return null;
            var ReturnValue = new ExpandoObject();
            var ReturnValueDictionary = ReturnValue as IDictionary<string, object?>;

            foreach (PropertyInfo Property in value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var PropertyValue = Property.GetValue(value);
                Type? PropertyType = PropertyValue?.GetType();
                if (PropertyValue is ExpandoObject PropertyValueExpando)
                {
                    PropertyValue = PropertyValueExpando.ConvertToExpando();
                }
                else if (PropertyValue is IEnumerable<object?> PropertyValueEnumerable)
                {
                    var TempList = new List<object?>();
                    foreach (var Item in PropertyValueEnumerable)
                    {
                        TempList.Add(Item.ConvertToExpando());
                    }
                    PropertyValue = TempList;
                }
                else if ((PropertyType?.IsClass ?? false) && PropertyType != typeof(string))
                {
                    PropertyValue = PropertyValue.ConvertToExpando();
                }
                ReturnValueDictionary[Property.Name.ToString(StringCase.CamelCase)] = PropertyValue;
            }

            return ReturnValue;
        }

        /// <summary>
        /// Converts to expando.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The resulting object</returns>
        public static ExpandoObject? ConvertToExpando(this IDictionary<string, object?>? value)
        {
            if (value is null)
                return null;
            var ReturnValue = new ExpandoObject();
            var ReturnValueDictionary = ReturnValue as IDictionary<string, object?>;

            foreach (var Key in value.Keys)
            {
                var PropertyValue = value[Key];
                PropertyValue = PropertyValue is ExpandoObject PropertyValueExpando
                    ? PropertyValueExpando.ConvertToExpando()
                    : (object?)PropertyValue.ConvertToExpando();
                ReturnValueDictionary[Key.ToString(StringCase.CamelCase)] = PropertyValue;
            }

            return ReturnValue;
        }
    }
}