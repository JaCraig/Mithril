using System.Reflection;

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
            PropertyInfo[] PublicProperties = objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Properties = new PropertyMetadata[PublicProperties.Length];
            for (var x = 0; x < PublicProperties.Length; ++x)
            {
                PropertyInfo? Property = PublicProperties[x];
                Properties[x] = new PropertyMetadata(Property);
            }
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        public PropertyMetadata[] Properties { get; }
    }
}