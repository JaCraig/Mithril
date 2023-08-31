using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Admin.ViewModels;
using Mithril.Security.Models;

namespace Mithril.Security.Admin
{
    /// <summary>
    /// Tenant Editor
    /// </summary>
    /// <seealso cref="EntityEditorBaseClass&lt;TenantVM, Tenant&gt;" />
    public class TenantEditor : EntityEditorBaseClass<TenantVM, Tenant>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantEditor"/> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="entityMetadataService">The entity metadata service.</param>
        /// <param name="dataType">Type of the data.</param>
        public TenantEditor(IDataService? dataService, IEntityMetadataService? entityMetadataService, string? dataType = null)
            : base(dataService, entityMetadataService, dataType)
        {
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public override string Icon { get; } = "fas fa-users";

        /// <summary>
        /// Converts the model into the appropriate view model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        /// <returns>
        /// The view model
        /// </returns>
        protected override IEntity Convert(Tenant model, bool full = true)
        {
            return new TenantVM(model);
        }

        /// <summary>
        /// Filters the query by search term.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>
        /// The resulting query.
        /// </returns>
        protected override IQueryable<Tenant>? FilterQueryBySearchQuery(IQueryable<Tenant>? query, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery)
                ? query
                : (query?.Where(permission => permission.DisplayName.StartsWith(searchQuery)));
        }
    }
}