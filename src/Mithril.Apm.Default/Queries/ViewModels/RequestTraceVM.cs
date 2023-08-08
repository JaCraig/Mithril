using BigBook;
using Mithril.Apm.Default.Models;

namespace Mithril.Apm.Default.Queries.ViewModels
{
    /// <summary>
    /// RequestTrace VM
    /// </summary>
    public class RequestTraceVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTraceVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public RequestTraceVM(RequestTrace? model)
        {
            if (model is null)
                return;
            DateCreated = model.DateCreated;
            MetaData = model.MetaData.ToList(x => new RequestMetaDataVM(x));
            Metrics = model.Metrics.ToList(x => new RequestMetricVM(x));
            TraceIdentifier = model.TraceIdentifier;
        }

        /// <summary>
        /// Gets the date created.
        /// </summary>
        /// <value>The date created.</value>
        public DateTime DateCreated { get; }

        /// <summary>
        /// Gets the meta data.
        /// </summary>
        /// <value>The meta data.</value>
        public List<RequestMetaDataVM> MetaData { get; } = new List<RequestMetaDataVM>();

        /// <summary>
        /// Gets the metrics.
        /// </summary>
        /// <value>The metrics.</value>
        public List<RequestMetricVM> Metrics { get; } = new List<RequestMetricVM>();

        /// <summary>
        /// Gets the trace identifier.
        /// </summary>
        /// <value>The trace identifier.</value>
        public string? TraceIdentifier { get; }
    }
}