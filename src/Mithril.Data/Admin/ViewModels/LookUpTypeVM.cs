using BigBook;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Data.Abstractions.Services;
using Mithril.Data.Models.General;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Data.Admin.ViewModels
{
    /// <summary>
    /// LookUpType view model class
    /// </summary>
    /// <seealso cref="EntityBaseClass&lt;LookUpType&gt;" />
    public class LookUpTypeVM : EntityBaseClass<LookUpType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpTypeVM"/> class.
        /// </summary>
        public LookUpTypeVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpTypeVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        public LookUpTypeVM(LookUpType model, bool full)
            : base(model)
        {
            if (model is null)
                return;
            DisplayName = model.DisplayName;
            Description = model.Description;
            LookUps = model.LookUps.ToList(lookUp => new LookUpVM(lookUp));
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [MaxLength(500)]
        [Order(2)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [MaxLength(64)]
        [Order(1)]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the look ups.
        /// </summary>
        /// <value>
        /// The look ups.
        /// </value>
        [Order(3)]
        public List<LookUpVM> LookUps { get; set; } = [];

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
        public override async Task<LookUpType?> SaveAsync(long id, IDataService? dataService, IServiceProvider? serviceProvider, ClaimsPrincipal? currentUser)
        {
            if (string.IsNullOrEmpty(DisplayName) || dataService is null)
                return null;
            var Model = LookUpType.Load(id, dataService) ?? new LookUpType(DisplayName, Description);
            Model.Description = Description;
            Model.DisplayName = DisplayName;
            Model.Active = Active;
            var CurrentTime = DateTime.UtcNow;
            foreach (var LookUp in LookUps ?? Enumerable.Empty<LookUpVM>())
            {
                LookUp.Save(Model);
            }
            var LookUpsToDelete = Model.LookUps.Where(lookUp => lookUp.DateModified < CurrentTime).ToArray();
            var Tasks = new List<Task>();
            await dataService.DeleteAsync(currentUser, LookUpsToDelete).ConfigureAwait(false);
            await Model.SaveAsync(dataService, currentUser).ConfigureAwait(false);
            return Model;
        }
    }
}