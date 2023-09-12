using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Admin.ViewModels;
using Mithril.Security.Models;

namespace Mithril.Security.Admin
{
    /// <summary>
    /// Permission editor service
    /// </summary>
    /// <seealso cref="EntityEditorBaseClass&lt;PermissionVM, Permission&gt;" />
    public class PermissionEditor : EntityEditorBaseClass<PermissionVM, Permission>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionEditor" /> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="entityMetadataService">The entity metadata service.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="dataType">Type of the data.</param>
        public PermissionEditor(IDataService? dataService, IEntityMetadataService? entityMetadataService, IServiceProvider? serviceProvider, string? dataType = null) : base(dataService, entityMetadataService, serviceProvider, dataType)
        {
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public override string Icon { get; } = "fas fa-lock";

        /// <summary>
        /// Converts the model into the appropriate view model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        /// <returns>The view model</returns>
        protected override IEntity Convert(Permission model, bool full = true) => new PermissionVM(model);

        /// <summary>
        /// Filters the query by search term.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>The resulting query.</returns>
        protected override IQueryable<Permission>? FilterQueryBySearchQuery(IQueryable<Permission>? query, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery)
                ? query
                : (query?.Where(permission => permission.DisplayName.Contains(searchQuery)));
        }
    }
}