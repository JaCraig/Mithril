using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;

namespace Mithril.Admin.Services
{
    /// <summary>
    /// Entity metadata service
    /// TODO: Add Tests
    /// </summary>
    /// <seealso cref="IEntityMetadataService"/>
    public class EntityMetadataService : IEntityMetadataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMetadataService"/> class.
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
        private Dictionary<Type, EntityMetadata> Entities { get; } = new Dictionary<Type, EntityMetadata>();

        /// <summary>
        /// Gets the metadata builders.
        /// </summary>
        /// <value>The metadata builders.</value>
        private IEnumerable<IMetadataBuilder> MetadataBuilders { get; }

        /// <summary>
        /// The lock object
        /// </summary>
        private readonly object LockObj = new();

        /// <summary>
        /// Extracts the metadata defining this entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The metadata for the entity type.</returns>
        public EntityMetadata ExtractMetadata<TEntity>() => ExtractMetadata(typeof(TEntity));

        /// <summary>
        /// Extracts the metadata defining this entity.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>The metadata for the entity type.</returns>
        public EntityMetadata ExtractMetadata(Type entityType)
        {
            if (Entities.TryGetValue(entityType, out EntityMetadata? ReturnValue))
                return ReturnValue;
            lock (LockObj)
            {
                if (Entities.TryGetValue(entityType, out ReturnValue))
                    return ReturnValue;
                ReturnValue = new EntityMetadata(entityType);
                foreach (PropertyMetadata Property in ReturnValue.Properties)
                {
                    foreach (IMetadataBuilder MetadataBuilder in MetadataBuilders)
                    {
                        _ = MetadataBuilder.ExtractMetadata(Property);
                    }
                }

                Entities.Add(entityType, ReturnValue);
                return ReturnValue;
            }
        }
    }
}