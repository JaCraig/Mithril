using BigBook;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Abstractions.Enums;
using Mithril.Security.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Security.Admin.ViewModels
{
    /// <summary>
    /// Permission VM class
    /// TODO: Add tests.
    /// </summary>
    /// <seealso cref="EntityBaseClass&lt;Permission&gt;" />
    public class PermissionVM : EntityBaseClass<Permission>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionVM"/> class.
        /// </summary>
        public PermissionVM()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public PermissionVM(Permission model)
            : base(model)
        {
            if (model is null)
            {
                DisplayName = Guid.NewGuid().ToString();
                return;
            }
            _ = Claims.Add(model.Claims.ForEach(claim => new ClaimDropDownVM(claim)));
            DisplayName = string.IsNullOrEmpty(model.DisplayName) ? Guid.NewGuid().ToString() : model.DisplayName;
            Operand = model.Operand;
        }

        /// <summary>
        /// Gets or sets the claims.
        /// </summary>
        /// <value>The claims.</value>
        [Order(3)]
        public List<ClaimDropDownVM> Claims { get; set; } = new List<ClaimDropDownVM>();

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        [Order(1)]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the operand.
        /// </summary>
        /// <value>The operand.</value>
        [Order(2)]
        public PermissionType Operand { get; set; }

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>The async task.</returns>
        public override async Task<Permission?> SaveAsync(long id, IDataService dataService, ClaimsPrincipal? currentUser)
        {
            Permission? Permission = Permission.Load(id, dataService);
            if (Permission is null)
                return null;
            if (string.IsNullOrEmpty(DisplayName))
            {
                Permission?.DeleteAsync(dataService, currentUser, false);
                return null;
            }
            Permission ??= (await Permission.LoadOrCreateAsync(DisplayName ?? "", Operand, Array.Empty<IUserClaim>(), dataService, currentUser).ConfigureAwait(false)) as Permission;
            if (Permission is null)
                return null;
            Permission.DisplayName = DisplayName;
            Permission.Claims.Clear();
            Permission.Operand = Operand;
            for (int I = 0, ClaimsCount = Claims.Count; I < ClaimsCount; I++)
            {
                ClaimDropDownVM Claim = Claims[I];
                _ = Permission.AddClaim(UserClaim.Load(Claim.Claim, dataService));
            }
            await Permission.SaveAsync(dataService, currentUser).ConfigureAwait(false);
            return Permission;
        }
    }
}