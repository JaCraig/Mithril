using Inflatable.BaseClasses;
using Mithril.Data.Inflatable.Databases;

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
            Reference(x => x.DisplayName);
            Reference(x => x.MetaData);
            Reference(x => x.Metric).WithMaxLength(4);
        }
    }
}