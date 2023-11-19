using Mithril.Navigation.Models;
using System.Security.Claims;

namespace Mithril.Navigation.Queries
{
    /// <summary>
    /// MenuVM
    /// </summary>
    public class MenuVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuVM" /> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="user">The user.</param>
        public MenuVM(Menu? data, ClaimsPrincipal? user)
        {
            if (data?.CanBeViewedBy(user) != true)
                return;
            Display = data.Display;
            MenuItems = data.Items.Where(x => x.CanBeViewedBy(user)).Select(x => new MenuItemVM(x)).ToList();
        }

        /// <summary>
        /// Gets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public string? Display { get; }

        /// <summary>
        /// Gets the menu items.
        /// </summary>
        /// <value>
        /// The menu items.
        /// </value>
        public List<MenuItemVM> MenuItems { get; } = [];
    }
}