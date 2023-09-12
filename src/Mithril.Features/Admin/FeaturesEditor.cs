using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using Mithril.Data.Abstractions.Services;
using Mithril.Features.Admin.ViewModels;
using Mithril.Features.Models;

namespace Mithril.Features.Admin
{
    /// <summary>
    /// Feature editor class
    /// </summary>
    /// <seealso cref="EntityEditorBaseClass&lt;Feature, FeatureVM&gt;" />
    public class FeatureEditor : EntityEditorBaseClass<FeatureVM, Feature>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureEditor" /> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="entityMetadataService">The entity metadata service.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="dataType">Type of the data.</param>
        public FeatureEditor(IDataService? dataService, IEntityMetadataService? entityMetadataService, IServiceProvider? serviceProvider, string? dataType = null)
            : base(dataService, entityMetadataService, serviceProvider, dataType)
        {
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public override string Icon { get; } = "fas fa-list-check";

        /// <summary>
        /// Converts the model into the appropriate view model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        /// <returns>The view model</returns>
        protected override IEntity Convert(Feature model, bool full = true) => new FeatureVM(model, full);

        /// <summary>
        /// Filters the query by search term.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>The resulting query.</returns>
        protected override IQueryable<Feature>? FilterQueryBySearchQuery(IQueryable<Feature>? query, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery)
                ? query
                : (query?.Where(Feature => Feature.Name.Contains(searchQuery)));
        }
    }
}