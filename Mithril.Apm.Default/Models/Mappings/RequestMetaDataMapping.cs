using Inflatable.BaseClasses;
using Mithril.Data.Inflatable.Databases;

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
            Reference(x => x.DisplayName);
            Reference(x => x.MetaData);
        }
    }
}