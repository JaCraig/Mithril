using Mithril.Navigation.Models;

namespace Mithril.Navigation.Queries
{
    /// <summary>
    /// MenuItemVM
    /// </summary>
    public class MenuItemVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemVM"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public MenuItemVM(MenuItem? data)
        {
            if (data is null)
                return;
            Description = data.Description;
            Url = data.Url;
            Display = data.Display;
            Icon = data.Icon;
            Order = data.Order;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string? Description { get; }

        /// <summary>
        /// Gets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public string? Display { get; }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string? Icon { get; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string? Url { get; }
    }
}