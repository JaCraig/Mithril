using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Apm.Default.Models.Mappings
{
    /// <summary>
    /// Request MetaData mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;RequestMetaData, DefaultDatabase&gt;"/>
    public class RequestMetaDataMapping : MappingBaseClass<RequestMetaData, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMetaDataMapping"/> class.
        /// </summary>
        public RequestMetaDataMapping()
        {
            _ = Reference(x => x.DisplayName);
            _ = Reference(x => x.MetaData);
        }
    }
}