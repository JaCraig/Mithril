using Mithril.Security.Abstractions.Interfaces;

namespace Mithril.Navigation.Abstractions.Interfaces
{
    /// <summary>
    /// Menu builder interface
    /// </summary>
    public interface IMenuBuilder
    {
        /// <summary>
        /// Gets the display name of the menu.
        /// </summary>
        /// <value>
        /// The display name of the menu.
        /// </value>
        string Display { get; }

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        IPermission? Permissions { get; }

        /// <summary>
        /// Sets the security.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>This.</returns>
        IMenuBuilder SetSecurity(IPermission? permission);

        /// <summary>
        /// Adds a menu item.
        /// </summary>
        /// <param name="display">The display.</param>
        /// <param name="description">The description.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="url">The URL.</param>
        /// <param name="order">The order.</param>
        /// <param name="permission">The permission.</param>
        /// <returns>This</returns>
        IMenuBuilder AddOrUpdateMenuItem(string display, string description, string icon, string url, int order = 0, IPermission? permission = null);

        /// <summary>
        /// Determines whether [the menu] [has the menu item specified].
        /// </summary>
        /// <param name="display">The display name.</param>
        /// <returns>
        ///   <c>true</c> if it [has the menu item specified]; otherwise, <c>false</c>.
        /// </returns>
        bool HasMenuItem(string display);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The async task.</returns>
        Task BuildAsync();
    }
}