using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using Mithril.Data.Abstractions.Enums;
using Mithril.Data.Abstractions.Services;
using Mithril.Data.Models.General;
using Mithril.Security.Admin.ViewModels;
using Mithril.Security.Models;

namespace Mithril.Security.Admin
{
    /// <summary>
    /// User editor
    /// </summary>
    /// <seealso cref="EntityEditorBaseClass&lt;UserVM, User&gt;" />
    public class UserEditor : EntityEditorBaseClass<UserVM, User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserEditor"/> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="entityMetadataService">The entity metadata service.</param>
        /// <param name="dataType">Type of the data.</param>
        public UserEditor(IDataService? dataService, IEntityMetadataService? entityMetadataService, string? dataType = null)
            : base(dataService, entityMetadataService, dataType)
        {
            ContactInfoType = LookUpType.Load(LookUpTypeEnum.ContactInfoType, DataService);
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public override string Icon { get; } = "fas fa-user";

        /// <summary>
        /// Gets the type of the contact information.
        /// </summary>
        /// <value>
        /// The type of the contact information.
        /// </value>
        private LookUpType? ContactInfoType { get; set; }

        /// <summary>
        /// Converts the model into the appropriate view model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        /// <returns>
        /// The view model
        /// </returns>
        protected override IEntity Convert(User model, bool full = true)
        {
            if (full)
                ContactInfoType = LookUpType.Load(LookUpTypeEnum.ContactInfoType, DataService);
            return new UserVM(model, full, ContactInfoType);
        }

        /// <summary>
        /// Filters the query by search term.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>
        /// The resulting query.
        /// </returns>
        protected override IQueryable<User>? FilterQueryBySearchQuery(IQueryable<User>? query, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery)
                ? query
                : (query?.Where(permission => permission.UserName.StartsWith(searchQuery) || permission.FirstName.StartsWith(searchQuery) || permission.LastName.StartsWith(searchQuery)));
        }
    }
}