using BigBook;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Navigation.Models;
using Mithril.Routing.Abstractions.Admin.DropDowns;
using Mithril.Security.Abstractions.Admin.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Mithril.Navigation.Admin.ViewModels
{
    /// <summary>
    /// MenuItem VM
    /// </summary>
    public class MenuItemVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemVM"/> class.
        /// </summary>
        public MenuItemVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemVM"/> class.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        public MenuItemVM(MenuItem? menuItem)
        {
            if (menuItem is null)
                return;
            Description = menuItem.Description;
            Display = menuItem.Display;
            Icon = menuItem.Icon;
            Order = menuItem.Order;
            WhoCanView = menuItem.Permissions?.Claims.ToList(claim => new ClaimDropDownVM(claim)) ?? new List<ClaimDropDownVM>();
            Url = menuItem.Url;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [MaxLength(1024)]
        [Order(5)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        [Required]
        [MaxLength(128)]
        [MinLength(1)]
        [Order(2)]
        public string? Display { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        [MaxLength(64)]
        [Order(4)]
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        [Range(0, int.MaxValue)]
        [Order(1)]
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [MaxLength(1024)]
        [Order(3)]
        [Query(typeof(RouteDropDown))]
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the who can view.
        /// </summary>
        /// <value>
        /// The who can view.
        /// </value>
        [Order(6)]
        public List<ClaimDropDownVM> WhoCanView { get; set; } = new List<ClaimDropDownVM>();
    }
}