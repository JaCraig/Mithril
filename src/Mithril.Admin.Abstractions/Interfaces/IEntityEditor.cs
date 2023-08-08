using System.Dynamic;
using System.Security.Claims;

namespace Mithril.Admin.Abstractions.Interfaces
{
    /// <summary>
    /// Entity editor
    /// </summary>
    /// <seealso cref="IEditor"/>
    public interface IEntityEditor : IEditor
    {
        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        /// <value>The type of the entity.</value>
        string EntityType { get; }

        /// <summary>
        /// Activates the entity specified asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>True if it succeeds, false otherwise.</returns>
        Task<bool> ActivateAsync(long id, ClaimsPrincipal? currentUser);

        /// <summary>
        /// Deletes the entity asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>True if it succeeds, false otherwise.</returns>
        Task<bool> DeleteEntityAsync(long id, ClaimsPrincipal? currentUser);

        /// <summary>
        /// Loads the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>The entity specified.</returns>
        IEntity? Load(long id, ExpandoObject? model, ClaimsPrincipal? currentUser);

        /// <summary>
        /// Loads the page asynchronously.
        /// </summary>
        /// <param name="page">The page (zero based).</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortAscending">if set to <c>true</c> [sort ascending].</param>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>The entities specified.</returns>
        Task<IEnumerable<IEntity>> LoadPageAsync(int page, int pageSize, string sortField, bool sortAscending, string searchQuery, ClaimsPrincipal? currentUser);

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>True if it succeeds, false otherwise.</returns>
        Task<bool> SaveEntityAsync(long id, ExpandoObject entity, ClaimsPrincipal? currentUser);

        /// <summary>
        /// Gets the total active items.
        /// </summary>
        /// <param name="currentUser">The current user.</param>
        /// <returns>The total.</returns>
        Task<int> TotalActiveAsync(ClaimsPrincipal? currentUser);
    }
}