using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

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
            _ = Reference(x => x.TraceIdentifier);
            _ = ManyToOne(x => x.MetaData).CascadeChanges();
            _ = ManyToOne(x => x.Metrics).CascadeChanges();
        }
    }
}