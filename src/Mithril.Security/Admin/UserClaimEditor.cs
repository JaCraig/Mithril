using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Admin.ViewModels;
using Mithril.Security.Models;

namespace Mithril.Security.Admin
{
    /// <summary>
    /// User claim editor
    /// </summary>
    /// <seealso cref="EntityEditorBaseClass&lt;UserClaimVM, UserClaim&gt;" />
    public class UserClaimEditor : EntityEditorBaseClass<UserClaimVM, UserClaim>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimEditor"/> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="entityMetadataService">The entity metadata service.</param>
        /// <param name="dataType">Type of the data.</param>
        public UserClaimEditor(IDataService? dataService, IEntityMetadataService? entityMetadataService, string? dataType = null)
            : base(dataService, entityMetadataService, dataType)
        {
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public override string Icon { get; } = "fas fa-id-card";

        /// <summary>
        /// Converts the model into the appropriate view model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        /// <returns>
        /// The view model
        /// </returns>
        protected override IEntity Convert(UserClaim model, bool full = true)
        {
            return new UserClaimVM(model);
        }

        /// <summary>
        /// Filters the query by search term.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>
        /// The resulting query.
        /// </returns>
        protected override IQueryable<UserClaim>? FilterQueryBySearchQuery(IQueryable<UserClaim>? query, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery)
                ? query
                : (query?.Where(permission => permission.Type.StartsWith(searchQuery) || permission.Value.StartsWith(searchQuery)));
        }
    }
}