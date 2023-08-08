using Mithril.Admin.Abstractions.DataEditor;
using System.Reflection;

namespace Mithril.Admin.Abstractions.Services
{
    /// <summary>
    /// Entity metadata service
    /// </summary>
    public interface IEntityMetadataService
    {
        /// <summary>
        /// Extracts the metadata defining this entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The metadata for the entity type.</returns>
        EntityMetadata? ExtractMetadata<TEntity>();

        /// <summary>
        /// Extracts the metadata defining this entity.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>The metadata for the entity type.</returns>
        EntityMetadata? ExtractMetadata(Type? entityType);

        /// <summary>
        /// Extracts the metadata defining this entity.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        /// The metadata for the entity type.
        /// </returns>
        EntityMetadata? ExtractMetadata(PropertyInfo? property);
    }
}