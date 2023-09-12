using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Security.Admin.ViewModels
{
    /// <summary>
    /// Tenant view model
    /// </summary>
    /// <seealso cref="EntityBaseClass&lt;Tenant&gt;" />
    public class TenantVM : EntityBaseClass<Tenant>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantVM"/> class.
        /// </summary>
        public TenantVM()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantVM" /> class.
        /// </summary>
        /// <param name="model">Model data</param>
        public TenantVM(Tenant model)
            : base(model)
        {
            if (model is null)
                return;
            DisplayName = model.DisplayName;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string? DisplayName { get; set; }

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
        public override async Task<Tenant?> SaveAsync(long id, IDataService? dataService, IServiceProvider? serviceProvider, ClaimsPrincipal? currentUser)
        {
            if (string.IsNullOrEmpty(DisplayName))
                return null;
            var Model = Tenant.Load(id, dataService) ?? new Tenant(DisplayName);
            Model.DisplayName = DisplayName;
            Model.Active = Active;
            await Model.SaveAsync(dataService, currentUser).ConfigureAwait(false);
            return Model;
        }
    }
}