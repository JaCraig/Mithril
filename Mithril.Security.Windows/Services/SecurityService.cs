using BigBook;
using Mithril.Core.Abstractions.Security.Enums;
using Mithril.Core.Abstractions.Security.Interfaces;
using Mithril.Core.Abstractions.Services;
using Mithril.Security.Windows.Models;

namespace Mithril.Security.Windows.Services
{
    /// <summary>
    /// Security service
    /// </summary>
    /// <seealso cref="ISecurityService"/>
    public class SecurityService : ISecurityService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityService"/> class.
        /// </summary>
        public SecurityService(IDataService dataService)
        {
            DataService = dataService;
        }

        /// <summary>
        /// Gets or sets the anonymous user account.
        /// </summary>
        /// <value>The anonymous user account.</value>
        private IUser? AnonymousUserAccount { get; set; }

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService DataService { get; }

        /// <summary>
        /// Gets or sets the system user account.
        /// </summary>
        /// <value>The system user account.</value>
        private IUser? SystemUserAccount { get; set; }

        /// <summary>
        /// The anon lock
        /// </summary>
        private readonly object AnonLock = new();

        /// <summary>
        /// The system lock object
        /// </summary>
        private readonly object SysLockObj = new();

        /// <summary>
        /// Loads all users.
        /// </summary>
        /// <returns>The users.</returns>
        public IEnumerable<IUser> LoadAllUsers()
        {
            return User.AllActive(DataService);
        }

        /// <summary>
        /// Loads the anonymous user account.
        /// </summary>
        /// <returns>The anonymous user account.</returns>
        public IUser LoadAnonymousUserAccount()
        {
            if (AnonymousUserAccount is not null)
                return AnonymousUserAccount;
            lock (AnonLock)
            {
                if (AnonymousUserAccount is not null)
                    return AnonymousUserAccount;
                var TempUser = User.Load("anonymous_account", DataService);
                if (TempUser is null)
                {
                    TempUser = new User("anonymous_account", "Anonymous", "Account");
                    AsyncHelper.RunSync(() => DataService.SaveAsync(TempUser));
                }
                AnonymousUserAccount = TempUser;
                return AnonymousUserAccount;
            }
        }

        /// <summary>
        /// Loads the user claim specified by the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user claim specified.</returns>
        public IUserClaim? LoadClaim(long id)
        {
            return UserClaim.Load(id, DataService);
        }

        /// <summary>
        /// Loads the user claim specified by the identifier.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns>The user claim specified.</returns>
        public IUserClaim? LoadClaim(UserClaimTypes type, string value)
        {
            return UserClaim.Load(type, value, DataService);
        }

        /// <summary>
        /// Loads the current user.
        /// </summary>
        /// <returns>The current user.</returns>
        public IUser LoadCurrentUser()
        {
            return User.LoadCurrentUser(DataService) ?? LoadAnonymousUserAccount();
        }

        /// <summary>
        /// Loads or creates the user claim asynchronously based on the type and value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns>The user claim specified.</returns>
        public Task<IUserClaim> LoadOrCreateClaimAsync(UserClaimTypes type, string value)
        {
            return UserClaim.LoadOrCreateAsync(type, value, DataService);
        }

        /// <summary>
        /// Loads or creates the permission asynchronously based on the display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="operand">The operand.</param>
        /// <param name="claims">The claims.</param>
        /// <returns>The permission specified.</returns>
        public Task<IPermission> LoadOrCreatePermissionAsync(string displayName, PermissionType operand, params IUserClaim[] claims)
        {
            return Permission.LoadOrCreateAsync(displayName, operand, claims, DataService);
        }

        /// <summary>
        /// Loads the permission specified.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns>The permission specified.</returns>
        public IPermission? LoadPermission(string displayName)
        {
            return Permission.Load(displayName, DataService);
        }

        /// <summary>
        /// Loads the permission specified.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>The permission specified.</returns>
        public IPermission? LoadPermission(SystemPermissions permission)
        {
            return LoadPermission((string)permission);
        }

        /// <summary>
        /// Loads the system account.
        /// </summary>
        /// <returns>The system account.</returns>
        public IUser LoadSystemAccount()
        {
            if (SystemUserAccount is not null)
                return SystemUserAccount;
            lock (SysLockObj)
            {
                if (SystemUserAccount is not null)
                    return SystemUserAccount;
                var TempUser = User.Load("system_account", DataService);
                if (TempUser is null)
                {
                    var AdminRole = AsyncHelper.RunSync(() => LoadOrCreateClaimAsync(UserClaimTypes.Role, "Admin"));
                    TempUser = new User("system_account", "System", "Account");
                    TempUser.AddClaim(AdminRole);
                    AsyncHelper.RunSync(() => DataService.SaveAsync(TempUser));
                }
                SystemUserAccount = TempUser;
                return SystemUserAccount;
            }
        }

        /// <summary>
        /// Loads the user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The user specified.</returns>
        public IUser? LoadUser(string username)
        {
            return User.Load(username, DataService);
        }

        /// <summary>
        /// Loads the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user specified.</returns>
        public IUser? LoadUser(long id)
        {
            return User.Load(id, DataService);
        }
    }
}