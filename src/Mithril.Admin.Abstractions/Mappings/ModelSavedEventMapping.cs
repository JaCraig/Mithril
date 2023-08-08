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
            Reference(x => x.Data);
            Reference(x => x.EntityID);
            Reference(x => x.EntityType);
        }
    }
}