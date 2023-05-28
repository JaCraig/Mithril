using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.Interfaces;

namespace Mithril.Admin.Abstractions.BaseClasses
{
    /// <summary>
    /// Metadata Builder base class
    /// </summary>
    /// <seealso cref="IMetadataBuilder"/>
    public abstract class MetadataBuilderBaseClass : IMetadataBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataBuilderBaseClass"/> class.
        /// </summary>
        protected MetadataBuilderBaseClass()
        { }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public virtual int Order { get; }

        /// <summary>
        /// Extracts metadata and adds it to the PropertyMetadata object.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        /// <returns>The resulting property metadata.</returns>
        public abstract PropertyMetadata? ExtractMetadata(PropertyMetadata? propertyMetadata);
    }
}