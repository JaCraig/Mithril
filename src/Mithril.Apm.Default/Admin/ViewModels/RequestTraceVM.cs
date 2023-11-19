using BigBook;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Apm.Default.Models;
using Mithril.Data.Abstractions.Services;
using System.Security.Claims;

namespace Mithril.Apm.Default.Admin.ViewModels
{
    /// <summary>
    /// Request trace VM
    /// </summary>
    /// <seealso cref="EntityBaseClass&lt;RequestTrace&gt;" />
    public class RequestTraceVM : EntityBaseClass<RequestTrace>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTraceVM"/> class.
        /// </summary>
        public RequestTraceVM()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTraceVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full"></param>
        public RequestTraceVM(RequestTrace? model, bool full = false)
            : base(model)
        {
            if (model is null)
                return;
            DateCreated = model.DateCreated;
            TraceIdentifier = model.TraceIdentifier;
            if (!full)
                return;
            MetaData = model.MetaData.ToList(x => new RequestMetaDataVM(x));
            Metrics = model.Metrics.ToList(x => new RequestMetricVM(x));
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
        public List<RequestMetaDataVM> MetaData { get; } = [];

        /// <summary>
        /// Gets the metrics.
        /// </summary>
        /// <value>The metrics.</value>
        public List<RequestMetricVM> Metrics { get; } = [];

        /// <summary>
        /// Gets the trace identifier.
        /// </summary>
        /// <value>The trace identifier.</value>
        public string? TraceIdentifier { get; }

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>
        /// The async task.
        /// </returns>
        public override Task<RequestTrace?> SaveAsync(long id, IDataService? dataService, IServiceProvider? serviceProvider, ClaimsPrincipal? currentUser) => Task.FromResult<RequestTrace?>(null);
    }
}