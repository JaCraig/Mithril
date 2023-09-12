using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Data.Abstractions.Enums;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Admin.DropDowns;
using Mithril.Security.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Security.Admin.ViewModels
{
    /// <summary>
    /// User claim view model
    /// </summary>
    /// <seealso cref="EntityBaseClass&lt;UserClaim&gt;" />
    public class UserClaimVM : EntityBaseClass<UserClaim>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimVM"/> class.
        /// </summary>
        public UserClaimVM()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimVM" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public UserClaimVM(UserClaim model)
            : base(model)
        {
            if (model is null)
                return;
            ClaimType = model.Type;
            Value = model.Value;
        }

        /// <summary>
        /// Gets or sets the type of the claim.
        /// </summary>
        /// <value>
        /// The type of the claim.
        /// </value>
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        [Query(typeof(ClaimTypesDropDown))]
        [Order(1)]
        public string? ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Required]
        [MinLength(1)]
        [Order(2)]
        public string? Value { get; set; }

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
        public override async Task<UserClaim?> SaveAsync(long id, IDataService? dataService, IServiceProvider? serviceProvider, ClaimsPrincipal? currentUser)
        {
            if (string.IsNullOrEmpty(ClaimType) || string.IsNullOrEmpty(Value))
                return null;
            var Model = UserClaim.Load(id, dataService) ?? new UserClaim(UserClaimTypes.GetEnum(ClaimType), Value);
            Model.Active = Active;
            Model.Type = UserClaimTypes.GetEnum(ClaimType);
            Model.Value = Value;
            await Model.SaveAsync(dataService, currentUser).ConfigureAwait(false);
            return Model;
        }
    }
}