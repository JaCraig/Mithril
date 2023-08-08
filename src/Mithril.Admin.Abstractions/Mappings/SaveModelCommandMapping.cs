using Inflatable.BaseClasses;
using Mithril.Admin.Abstractions.Commands;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Admin.Abstractions.Mappings
{
    /// <summary>
    /// Save model command mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;SaveModelCommand, DefaultDatabase&gt;"/>
    public class SaveModelCommandMapping : MappingBaseClass<SaveModelCommand, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveModelCommandMapping"/> class.
        /// </summary>
        public SaveModelCommandMapping()
        {
            Reference(x => x.Data);
            Reference(x => x.EntityID);
            Reference(x => x.EntityType);
        }
    }
}