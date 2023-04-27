using BigBook;
using Mithril.Data.Abstractions.Services;
using Mithril.Navigation.Abstractions.Interfaces;
using Mithril.Navigation.Models;
using Mithril.Security.Abstractions.Interfaces;
using System.Security.Claims;

namespace Mithril.Navigation.Services
{
    /// <summary>
    /// Menu builder
    /// </summary>
    /// <seealso cref="IMenuBuilder" />
    public class MenuBuilder : IMenuBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuBuilder" /> class.
        /// </summary>
        /// <param name="display">The display.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="user">The user.</param>
        public MenuBuilder(string display, IDataService? dataService, ClaimsPrincipal? user)
        {
            Display = display;
            DataService = dataService;
            User = user;
            InternalMenu = AsyncHelper.RunSync(() => Menu.LoadOrCreateAsync(display, dataService, user));
        }

        /// <summary>
        /// Gets the display name of the menu.
        /// </summary>
        /// <value>
        /// The display name of the menu.
        /// </value>
        public string Display { get; }

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public IPermission? Permissions => InternalMenu?.Permissions;

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>
        /// The data service.
        /// </value>
        private IDataService? DataService { get; }

        /// <summary>
        /// Gets the internal menu.
        /// </summary>
        /// <value>
        /// The internal menu.
        /// </value>
        private Menu InternalMenu { get; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        private ClaimsPrincipal? User { get; }

        /// <summary>
        /// Adds a menu item.
        /// </summary>
        /// <param name="display">The display.</param>
        /// <param name="description">The description.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="url">The URL.</param>
        /// <param name="order">The order.</param>
        /// <param name="permission">The permission.</param>
        /// <returns>
        /// This
        /// </returns>
        public IMenuBuilder AddOrUpdateMenuItem(string display, string description, string icon, string url, int order = 0, IPermission? permission = null)
        {
            InternalMenu?.AddOrUpdateMenuItem(display, description, icon, url, order, permission);
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// The async task.
        /// </returns>
        public Task BuildAsync()
        {
            return InternalMenu?.SaveAsync(DataService, User) ?? Task.CompletedTask;
        }

        /// <summary>
        /// Determines whether [the menu] [has the menu item specified].
        /// </summary>
        /// <param name="display">The display name.</param>
        /// <returns>
        /// <c>true</c> if it [has the menu item specified]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasMenuItem(string display)
        {
            return InternalMenu?.Items.Any(x => string.Equals(x.Display, display, StringComparison.OrdinalIgnoreCase)) ?? false;
        }

        /// <summary>
        /// Sets the security.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>
        /// This.
        /// </returns>
        public IMenuBuilder SetSecurity(IPermission? permission)
        {
            if (InternalMenu is null)
                return this;
            InternalMenu.Permissions = permission;
            return this;
        }
    }
}