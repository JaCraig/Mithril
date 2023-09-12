using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Data.Abstractions.Services;
using Mithril.Features.Models;
using System.Security.Claims;

namespace Mithril.Features.Admin.ViewModels
{
    /// <summary>
    /// Feature VM
    /// </summary>
    /// <seealso cref="EntityBaseClass&lt;Feature&gt;" />
    public class FeatureVM : EntityBaseClass<Feature>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureVM"/> class.
        /// </summary>
        public FeatureVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        public FeatureVM(Feature model, bool full)
            : base(model)
        {
            if (model is null)
                return;
            Name = model.Name;
            Description = model.Description;
            Category = model.Category;
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [Order(2)]
        public string? Category { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Order(3)]
        public string? Description { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Order(1)]
        public string? Name { get; }

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
        public override async Task<Feature?> SaveAsync(long id, IDataService? dataService, IServiceProvider? serviceProvider, ClaimsPrincipal? currentUser)
        {
            var FeatureManager = serviceProvider?.GetService<ISessionManager>();
            if (FeatureManager is null || string.IsNullOrEmpty(Name))
                return null;
            await new ToggleFeatureCommand(Name, Active).SaveAsync(dataService, currentUser).ConfigureAwait(false);
            return Feature.Load(Name, dataService);
        }
    }
}