using Mithril.Data.Abstractions.Enums;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Security.Abstractions.Enums;
using Mithril.Security.Abstractions.Interfaces;

namespace Mithril.Security.Abstractions.Services
{
    /// <summary>
    /// Permission service
    /// </summary>
    public interface ISecurityService
    {
        /// <summary>
        /// Loads all tenants.
        /// </summary>
        /// <returns>All tenants.</returns>
        IEnumerable<ITenant> LoadAllTenants();

        /// <summary>
        /// Loads all users.
        /// </summary>
        /// <returns>All users.</returns>
        IEnumerable<IUser> LoadAllUsers();

        /// <summary>
        /// Loads the anonymous user account.
        /// </summary>
        /// <returns>The anonymous user account.</returns>
        IUser? LoadAnonymousUserAccount();

        /// <summary>
        /// Loads the user claim specified by the identifier.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns>The user claim specified.</returns>
        IUserClaim? LoadClaim(UserClaimTypes type, string value);

        /// <summary>
        /// Loads the user claim specified by the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user claim specified.</returns>
        IUserClaim? LoadClaim(long id);

        /// <summary>
        /// Loads the current user.
        /// </summary>
        /// <returns>The current user.</returns>
        IUser? LoadCurrentUser();

        /// <summary>
        /// Loads or creates the user claim asynchronously based on the type and value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns>The user claim specified.</returns>
        Task<IUserClaim> LoadOrCreateClaimAsync(UserClaimTypes type, string value);

        /// <summary>
        /// Loads or creates the permission asynchronously based on the display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="operand">The operand.</param>
        /// <param name="claims">The claims.</param>
        /// <returns>The permission specified.</returns>
        Task<IPermission> LoadOrCreatePermissionAsync(string displayName, PermissionType operand, params IUserClaim[] claims);

        /// <summary>
        /// Loads the permission specified.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns>The permission specified.</returns>
        IPermission? LoadPermission(string displayName);

        /// <summary>
        /// Loads the permission specified.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>The permission specified.</returns>
        IPermission? LoadPermission(SystemPermissions permission);

        /// <summary>
        /// Loads the system account.
        /// </summary>
        /// <returns>The system account.</returns>
        IUser? LoadSystemAccount();

        /// <summary>
        /// Loads the system tenant.
        /// </summary>
        /// <returns>The system tenant.</returns>
        ITenant? LoadSystemTenant();
    }
}