using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Data.Abstractions.Interfaces;
using System.Security.Claims;

namespace Mithril.Admin.Abstractions.Interfaces
{
    /// <summary>
    /// Entity interface
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IEntity"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        [DoNotList]
        bool Active { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [ReadOnly]
        [DoNotList]
        long ID { get; set; }
    }

    /// <summary>
    /// IEntity interface
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IEntity<TModel> : IEntity
        where TModel : IModel
    {
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
        Task<TModel?> SaveAsync(long id, Data.Abstractions.Services.IDataService? dataService, IServiceProvider? serviceProvider, ClaimsPrincipal? currentUser);
    }
}