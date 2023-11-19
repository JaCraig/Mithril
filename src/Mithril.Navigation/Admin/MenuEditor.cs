using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using Mithril.Data.Abstractions.Services;
using Mithril.Navigation.Admin.ViewModels;
using Mithril.Navigation.Models;

namespace Mithril.Navigation.Admin
{
    /// <summary>
    /// Menu editor
    /// </summary>
    /// <seealso cref="EntityEditorBaseClass&lt;MenuVM, Menu&gt;" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="MenuEditor" /> class.
    /// </remarks>
    /// <param name="dataService">The data service.</param>
    /// <param name="entityMetadataService">The entity metadata service.</param>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="dataType">Type of the data.</param>
    public class MenuEditor(IDataService? dataService, IEntityMetadataService? entityMetadataService, IServiceProvider? serviceProvider, string? dataType = null) : EntityEditorBaseClass<MenuVM, Menu>(dataService, entityMetadataService, serviceProvider, dataType)
    {
        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public override string Icon { get; } = "fas fa-bars";

        /// <summary>
        /// Converts the model into the appropriate view model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        /// <returns>The view model</returns>
        protected override IEntity Convert(Menu model, bool full = true) => new MenuVM(model, full);

        /// <summary>
        /// Filters the query by search term.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>The resulting query.</returns>
        protected override IQueryable<Menu>? FilterQueryBySearchQuery(IQueryable<Menu>? query, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery)
                ? query
                : (query?.Where(menu => menu.Display.Contains(searchQuery)));
        }
    }
}