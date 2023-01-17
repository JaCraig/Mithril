using Mithril.Data.Abstractions.Services;
using System.Security.Claims;
using Valkyrie;

namespace Mithril.Data.Abstractions.Interfaces
{
    /// <summary>
    /// IModel interface
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    /// <seealso cref="IModel"/>
    public interface IModel<TClass> : IModel, IEquatable<IModel<TClass>>
        where TClass : IModel<TClass>, new()
    {
        /// <summary>
        /// Setups the object and returns it.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>This.</returns>
        TClass SetupObjectAndReturn(IDataService dataService, ClaimsPrincipal? currentUser);
    }

    /// <summary>
    /// Model interface
    /// </summary>
    /// <seealso cref="IEquatable&lt;IModel&gt;"/>
    public interface IModel : IEquatable<IModel>
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IModel"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        bool Active { get; set; }

        /// <summary>
        /// Gets or sets the creator.
        /// </summary>
        /// <value>The creator.</value>
        IUser? Creator { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        [Between("1/1/1900", "1/1/2100")]
        DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        [Between("1/1/1900", "1/1/2100")]
        DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        long ID { get; set; }

        /// <summary>
        /// Gets or sets the modifier.
        /// </summary>
        /// <value>The modifier.</value>
        IUser? Modifier { get; set; }

        /// <summary>
        /// Gets or sets the tenant associated with the object.
        /// </summary>
        /// <value>The tenant associated with the object.</value>
        long TenantID { get; set; }

        /// <summary>
        /// Determines whether this instance [can be modified by] the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// <c>true</c> if this instance [can be modified by] the specified user; otherwise, <c>false</c>.
        /// </returns>
        bool CanBeModifiedBy(ClaimsPrincipal? user);

        /// <summary>
        /// Determines whether this instance [can be viewed by] the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// <c>true</c> if this instance [can be viewed by] the specified user; otherwise, <c>false</c>.
        /// </returns>
        bool CanBeViewedBy(ClaimsPrincipal? user);

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        /// <param name="softDelete">if set to <c>true</c> [soft delete].</param>
        /// <returns></returns>
        Task DeleteAsync(IDataService dataService, ClaimsPrincipal? currentUser, bool softDelete = true);

        /// <summary>
        /// Saves this instance asynchronously.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>The async task.</returns>
        Task SaveAsync(IDataService dataService, ClaimsPrincipal? currentUser);

        /// <summary>
        /// Sets up the object.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        void SetupObject(IDataService? dataService, ClaimsPrincipal? currentUser);

        /// <summary>
        /// Sets up the object.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        void SetupObject(IDataService? dataService, IUser? currentUser);
    }
}