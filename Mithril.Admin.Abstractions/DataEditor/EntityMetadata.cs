using BigBook;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Mithril.Admin.Abstractions.DataEditor
{
    /// <summary>
    /// Entity metadata
    /// TODO: Add Tests
    /// </summary>
    public class EntityMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMetadata"/> class.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        public EntityMetadata(Type objectType)
        {
            if (objectType is null)
                return;
            Name = objectType.GetName();
            DisplayName = SplitCamelCase(Name);
            PropertyInfo[] PublicProperties = GetProperties(objectType);
            Properties = new PropertyMetadata[PublicProperties.Length];
            for (var x = 0; x < PublicProperties.Length; ++x)
            {
                PropertyInfo? Property = PublicProperties[x];
                Properties[x] = new PropertyMetadata(Property);
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; } = "";

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; } = "";

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        public PropertyMetadata[] Properties { get; } = Array.Empty<PropertyMetadata>();

        /// <summary>
        /// Filters the properties based on the JSON ignore attribute.
        /// </summary>
        /// <param name="propertyInfos">The property infos.</param>
        /// <returns>The filtered properties.</returns>
        private static PropertyInfo[] FilterProperties(PropertyInfo[] propertyInfos)
        {
            return propertyInfos.Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>() is null && x.GetCustomAttribute<IgnoreAttribute>() is null).ToArray();
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The properties on the object type.</returns>
        private static PropertyInfo[] GetProperties(Type type)
        {
            var EntityIEnumerableType = type.GetIEnumerableElementType();
            return OrderProperties(FilterProperties(EntityIEnumerableType == type
                ? type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                : EntityIEnumerableType.GetProperties(BindingFlags.Public | BindingFlags.Instance)));
        }

        /// <summary>
        /// Orders the properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns>The ordered properties</returns>
        private static PropertyInfo[] OrderProperties(PropertyInfo[] properties)
        {
            return properties is null
                ? Array.Empty<PropertyInfo>()
                : properties.OrderBy(x => x.GetCustomAttribute<OrderAttribute>()?.Order ?? (int.MaxValue / 2)).ThenBy(x => x.Name).ToArray();
        }

        /// <summary>
        /// Splits the camel case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Splits the camel case names</returns>
        private static string SplitCamelCase(string? input)
        {
            return input?.AddSpaces() ?? "";
        }
    }
}