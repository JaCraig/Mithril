using Inflatable.BaseClasses;
using Mithril.Admin.Abstractions.Events;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Admin.Abstractions.Mappings
{
    /// <summary>
    /// Model saved event mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;ModelSavedEvent, DefaultDatabase&gt;"/>
    public class ModelSavedEventMapping : MappingBaseClass<ModelSavedEvent, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelSavedEventMapping"/> class.
        /// </summary>
        public ModelSavedEventMapping()
        {
            _ = Reference(x => x.Data);
            _ = Reference(x => x.EntityID);
            _ = Reference(x => x.EntityType);
        }
    }
}