using BigBook;
using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Abstractions.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Navigation.Models
{
    /// <summary>
    /// Menu entry
    /// </summary>
    /// <seealso cref="ModelBase&lt;Menu&gt;"/>
    public class Menu : ModelBase<Menu>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        public Menu()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu" /> class.
        /// </summary>
        /// <param name="display">The display.</param>
        /// <exception cref="ArgumentNullException">display</exception>
        /// <exception cref="ArgumentException">display</exception>
        public Menu(string display)
            : this()
        {
            if (string.IsNullOrEmpty(display))
                throw new ArgumentNullException(nameof(display));
            if (display.Length > 64)
                throw new ArgumentException(nameof(display) + " is too long. 64 characters max allowed.");
            Display = display;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        public string? Display { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public virtual IList<MenuItem> Items { get; set; } = new List<MenuItem>();

        /// <summary>
        /// Gets or sets the permission.
        /// </summary>
        /// <value>The permission.</value>
        public virtual IPermission? Permissions { get; set; }

        /// <summary>
        /// Loads the menu by input path.
        /// </summary>
        /// <param name="display">The display.</param>
        /// <param name="dataService">The data service.</param>
        /// <returns>
        /// The route specified.
        /// </returns>
        public static Menu? Load(string? display, IDataService? dataService)
        {
            if (string.IsNullOrEmpty(display) || dataService is null)
                return null;
            return Query(dataService)?.Where(x => x.Display == display).FirstOrDefault();
        }

        /// <summary>
        /// Loads or creates the route.
        /// </summary>
        /// <param name="display">The display.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The route entry.
        /// </returns>
        public static async Task<Menu?> LoadOrCreateAsync(string display, IDataService? dataService, ClaimsPrincipal? user)
        {
            if (string.IsNullOrEmpty(display) || dataService is null)
                return null;
            var ReturnValue = Load(display, dataService);
            if (ReturnValue is null)
            {
                ReturnValue = new Menu(display);
                if (dataService is not null)
                    await dataService.SaveAsync(user, ReturnValue).ConfigureAwait(false);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Menu left, Menu right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Menu left, Menu right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(Menu left, Menu right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Menu first, Menu second)
        {
            return ReferenceEquals(first, second)
                || (first is not null
                    && second is not null
                    && first.CompareTo(second) == 0);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(Menu left, Menu right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(Menu left, Menu right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(Menu? other)
        {
            return base.CompareTo(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(Menu other)
        {
            return base.Equals(other);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString() => Display ?? "";

        /// <summary>
        /// Adds the menu item.
        /// </summary>
        /// <param name="display">The display.</param>
        /// <param name="description">The description.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="url">The URL.</param>
        /// <param name="order">The order.</param>
        /// <param name="permission">The permission.</param>
        public void AddOrUpdateMenuItem(string display, string description, string icon, string url, int order = 0, IPermission? permission = null)
        {
            Items ??= new List<MenuItem>();
            var Item = Items.FirstOrDefault(x => string.Equals(x.Display, display, StringComparison.OrdinalIgnoreCase)) ?? Items.AddAndReturn(new MenuItem
            {
                Display = display,
                Description = description,
                Icon = icon,
                Url = url,
                Parent = this,
                Order = order,
                Permissions = permission
            });

            Item.Display = display;
            Item.Description = description;
            Item.Icon = icon;
            Item.Url = url;
            Item.Parent = this;
            Item.Order = order;
            Item.Permissions = permission;
        }

        /// <summary>
        /// Adds the menu item.
        /// </summary>
        /// <param name="display">The display.</param>
        /// <param name="description">The description.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="url">The URL.</param>
        /// <param name="order">The order.</param>
        /// <param name="permission">The permission.</param>
        public void AddOrUpdateMenuItem(string display, string description, string icon, Uri url, int order = 0, IPermission? permission = null)
        {
            AddOrUpdateMenuItem(display, description, icon, url?.ToString() ?? string.Empty, order, permission);
        }

        /// <summary>
        /// Determines whether this instance [can be viewed by] the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// <c>true</c> if this instance [can be viewed by] the specified user; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanBeViewedBy(ClaimsPrincipal? user)
        {
            return (Permissions?.HasPermission(user) ?? true) && base.CanBeViewedBy(user);
        }
    }
}