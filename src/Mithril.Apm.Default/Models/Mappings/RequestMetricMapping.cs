using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Apm.Default.Models.Mappings
{
    /// <summary>
    /// Request Metric mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;RequestMetric, DefaultDatabase&gt;"/>
    public class RequestMetricMapping : MappingBaseClass<RequestMetric, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMetricMapping"/> class.
        /// </summary>
        public RequestMetricMapping()
        {
            _ = Reference(x => x.DisplayName);
            _ = Reference(x => x.MetaData);
            _ = Reference(x => x.Metric).WithMaxLength(4);
        }
    }
}