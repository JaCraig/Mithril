using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using Mithril.Apm.Default.Admin.ViewModels;
using Mithril.Apm.Default.Models;
using Mithril.Data.Abstractions.Services;

namespace Mithril.Apm.Default.Admin
{
    /// <summary>
    /// Request trace editor
    /// </summary>
    /// <seealso cref="EntityEditorBaseClass&lt;RequestTraceVM, RequestTrace&gt;" />
    public class RequestTraceEditor : EntityEditorBaseClass<RequestTraceVM, RequestTrace>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTraceEditor" /> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="entityMetadataService">The entity metadata service.</param>
        /// <param name="dataType">Type of the data.</param>
        public RequestTraceEditor(IDataService? dataService, IEntityMetadataService? entityMetadataService, string? dataType = null)
            : base(dataService, entityMetadataService, dataType)
        {
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public override string Icon { get; } = "fas fa-user-secret";

        /// <summary>
        /// Converts the model into the appropriate view model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        /// <returns>The view model</returns>
        protected override IEntity Convert(RequestTrace model, bool full = true) => new RequestTraceVM(model, full);

        /// <summary>
        /// Filters the query by search term.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>The resulting query.</returns>
        protected override IQueryable<RequestTrace>? FilterQueryBySearchQuery(IQueryable<RequestTrace>? query, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery)
                ? query
                : (query?.Where(requestTrace => requestTrace.TraceIdentifier.Contains(searchQuery)));
        }
    }
}