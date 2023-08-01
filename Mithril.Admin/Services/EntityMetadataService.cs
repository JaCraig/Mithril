using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using System.Collections.Concurrent;
using System.Reflection;

namespace Mithril.Admin.Services
{
    /// <summary>
    /// Entity metadata service
    /// </summary>
    /// <seealso cref="IEntityMetadataService"/>
    public class EntityMetadataService : IEntityMetadataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMetadataService" /> class.
        /// </summary>
        /// <param name="metadataBuilders">The metadata builders.</param>
        public EntityMetadataService(IEnumerable<IMetadataBuilder> metadataBuilders)
        {
            MetadataBuilders = metadataBuilders;
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>The entities.</value>
        private ConcurrentDictionary<Type, EntityMetadata> Entities { get; } = new ConcurrentDictionary<Type, EntityMetadata>();

        /// <summary>
        /// Gets the metadata builders.
        /// </summary>
        /// <value>The metadata builders.</value>
        private IEnumerable<IMetadataBuilder> MetadataBuilders { get; }

        /// <summary>
        /// Extracts the metadata defining this entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The metadata for the entity type.</returns>
        public EntityMetadata? ExtractMetadata<TEntity>() => ExtractMetadata(typeof(TEntity));

        /// <summary>
        /// Extracts the metadata defining this entity.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>The metadata for the entity type.</returns>
        public EntityMetadata? ExtractMetadata(Type? entityType)
        {
            if (entityType is null)
                return null;
            if (Entities.TryGetValue(entityType, out EntityMetadata? ReturnValue))
                return ReturnValue;
            ReturnValue = new EntityMetadata(entityType);
            Entities.AddOrUpdate(entityType, ReturnValue, (__, _) => ReturnValue);
            foreach (PropertyMetadata Property in ReturnValue.Properties)
            {
                foreach (IMetadataBuilder MetadataBuilder in MetadataBuilders)
                {
                    _ = MetadataBuilder.ExtractMetadata(Property, this);
                }
            }
            return ReturnValue;
        }

        /// <summary>
        /// Extracts the metadata defining this entity.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        /// The metadata for the entity type.
        /// </returns>
        public EntityMetadata? ExtractMetadata(PropertyInfo? property)
        {
            return ExtractMetadata(property?.PropertyType);
        }
    }
}