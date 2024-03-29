﻿using BigBook;
using Mithril.Data.Abstractions.Enums;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Abstractions;
using Mithril.Security.Abstractions.Enums;
using Mithril.Security.Abstractions.Interfaces;
using Mithril.Security.Abstractions.Services;
using Mithril.Security.Models;

namespace Mithril.Security.Services
{
    /// <summary>
    /// Security service
    /// </summary>
    /// <seealso cref="ISecurityService"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="SecurityService"/> class.
    /// </remarks>
    /// <param name="dataService">The data service.</param>
    /// <param name="systemAccounts">The system accounts.</param>
    public class SecurityService(IDataService? dataService, SystemAccounts? systemAccounts) : ISecurityService
    {
        /// <summary>
        /// Gets or sets the anonymous user account.
        /// </summary>
        /// <value>The anonymous user account.</value>
        private IUser? AnonymousUserAccount { get; set; }

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService? DataService { get; } = dataService;

        /// <summary>
        /// Gets the system accounts.
        /// </summary>
        /// <value>The system accounts.</value>
        private SystemAccounts? SystemAccounts { get; } = systemAccounts;

        /// <summary>
        /// Gets or sets the system tenant.
        /// </summary>
        /// <value>The system tenant.</value>
        private ITenant? SystemTenant { get; set; }

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
        /// The system tenant lock object
        /// </summary>
        private readonly object SysTenantLockObject = new();

        /// <summary>
        /// Loads all tenants.
        /// </summary>
        /// <returns>All tenants.</returns>
        public IEnumerable<ITenant> LoadAllTenants() => Tenant.AllActive(DataService);

        /// <summary>
        /// Loads all users.
        /// </summary>
        /// <returns>The users.</returns>
        public IEnumerable<IUser> LoadAllUsers() => User.AllActive(DataService);

        /// <summary>
        /// Loads the anonymous user account.
        /// </summary>
        /// <returns>The anonymous user account.</returns>
        public IUser? LoadAnonymousUserAccount()
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
                    ITenant? TempTenant = LoadSystemTenant();
                    if (TempTenant is null)
                        return null;
                    TempUser = new User("anonymous_account", "Anonymous", "Account", TempTenant);
                    TempTenant.Users.Add(TempUser);
                    AsyncHelper.RunSync(() => DataService?.SaveAsync(SystemAccounts?.SystemClaimsPrincipal, TempTenant) ?? Task.CompletedTask);
                    TempUser = User.Load("anonymous_account", DataService);
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
        public IUserClaim? LoadClaim(long id) => UserClaim.Load(id, DataService);

        /// <summary>
        /// Loads the user claim specified by the identifier.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns>The user claim specified.</returns>
        public IUserClaim? LoadClaim(UserClaimTypes type, string value) => UserClaim.Load(type, value, DataService);

        /// <summary>
        /// Loads the current user.
        /// </summary>
        /// <returns>The current user.</returns>
        public IUser? LoadCurrentUser() => User.LoadCurrentUser(DataService) ?? LoadAnonymousUserAccount();

        /// <summary>
        /// Loads or creates the user claim asynchronously based on the type and value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns>The user claim specified.</returns>
        public Task<IUserClaim> LoadOrCreateClaimAsync(UserClaimTypes type, string value) => UserClaim.LoadOrCreateAsync(type, value, DataService, SystemAccounts?.SystemClaimsPrincipal);

        /// <summary>
        /// Loads or creates the permission asynchronously based on the display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="operand">The operand.</param>
        /// <param name="claims">The claims.</param>
        /// <returns>The permission specified.</returns>
        public Task<IPermission> LoadOrCreatePermissionAsync(string displayName, PermissionType operand, params IUserClaim[] claims) => Permission.LoadOrCreateAsync(displayName, operand, claims, DataService, SystemAccounts?.SystemClaimsPrincipal);

        /// <summary>
        /// Loads the permission specified.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns>The permission specified.</returns>
        public IPermission? LoadPermission(string displayName) => Permission.Load(displayName, DataService);

        /// <summary>
        /// Loads the permission specified.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>The permission specified.</returns>
        public IPermission? LoadPermission(SystemPermissions permission) => LoadPermission((string)permission);

        /// <summary>
        /// Loads the system account.
        /// </summary>
        /// <returns>The system account.</returns>
        public IUser? LoadSystemAccount()
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
                    IUserClaim? AdminRole = AsyncHelper.RunSync(() => LoadOrCreateClaimAsync(UserClaimTypes.Role, "Admin"));
                    ITenant? TempTenant = LoadSystemTenant();
                    if (TempTenant is null)
                        return null;
                    TempUser = new User("system_account", "System", "Account", TempTenant);
                    _ = TempUser.AddClaim(AdminRole);
                    TempTenant.Users.Add(TempUser);
                    AsyncHelper.RunSync(() => DataService?.SaveAsync(SystemAccounts?.SystemClaimsPrincipal, TempTenant) ?? Task.CompletedTask);
                    TempUser = User.Load("system_account", DataService);
                }
                SystemUserAccount = TempUser;
                return SystemUserAccount;
            }
        }

        /// <summary>
        /// Loads the system tenant.
        /// </summary>
        /// <returns>The system tenant.</returns>
        public ITenant? LoadSystemTenant()
        {
            if (SystemTenant is not null)
                return SystemTenant;
            lock (SysTenantLockObject)
            {
                if (SystemTenant is not null)
                    return SystemTenant;
                var TempTenant = Tenant.Load("system_tenant", DataService);
                if (TempTenant is null)
                {
                    TempTenant = new Tenant("system_tenant");
                    AsyncHelper.RunSync(() => DataService?.SaveAsync(SystemAccounts?.SystemClaimsPrincipal, TempTenant) ?? Task.CompletedTask);
                    TempTenant.TenantID = TempTenant.ID;
                    AsyncHelper.RunSync(() => DataService?.SaveAsync(SystemAccounts?.SystemClaimsPrincipal, TempTenant) ?? Task.CompletedTask);
                    TempTenant = Tenant.Load("system_tenant", DataService);
                }
                SystemTenant = TempTenant;
                return SystemTenant;
            }
        }

        /// <summary>
        /// Loads the user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The user specified.</returns>
        public IUser? LoadUser(string username) => User.Load(username, DataService);

        /// <summary>
        /// Loads the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user specified.</returns>
        public IUser? LoadUser(long id) => User.Load(id, DataService);
    }
}