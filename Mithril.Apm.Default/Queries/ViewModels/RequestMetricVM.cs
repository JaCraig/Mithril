using Mithril.Apm.Default.Models;

namespace Mithril.Apm.Default.Queries.ViewModels
{
    /// <summary>
    /// RequestMetric VM
    /// </summary>
    public class RequestMetricVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMetricVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public RequestMetricVM(RequestMetric model)
        {
            if (model is null)
                return;
            DisplayName = model.DisplayName;
            MetaData = model.MetaData;
            Metric = model.Metric;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string? DisplayName { get; }

        /// <summary>
        /// Gets the meta data.
        /// </summary>
        /// <value>The meta data.</value>
        public string? MetaData { get; }

        /// <summary>
        /// Gets the metrics.
        /// </summary>
        /// <value>The metrics.</value>
        public decimal Metric { get; }
    }
}