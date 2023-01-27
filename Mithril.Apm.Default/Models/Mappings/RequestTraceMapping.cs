using Inflatable.BaseClasses;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Apm.Default.Models.Mappings
{
    /// <summary>
    /// Request trace mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;RequestTrace, DefaultDatabase&gt;"/>
    public class RequestTraceMapping : MappingBaseClass<RequestTrace, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTraceMapping"/> class.
        /// </summary>
        public RequestTraceMapping()
        {
            Reference(x => x.TraceIdentifier);
            ManyToOne(x => x.MetaData).CascadeChanges();
            ManyToOne(x => x.Metrics).CascadeChanges();
        }
    }
}