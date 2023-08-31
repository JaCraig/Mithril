using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Security.Abstractions.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Navigation.Models
{
    /// <summary>
    /// Menu item
    /// </summary>
    /// <seealso cref="IEquatable{MenuItem}"/>
    /// <seealso cref="ModelBase{MenuItem}"/>
    public class MenuItem : ModelBase<MenuItem>, IEquatable<MenuItem>
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [MaxLength(1024)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [Required]
        [MaxLength(128)]
        [MinLength(1)]
        public string? Display { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        [MaxLength(64)]
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        [Valkyrie.Compare(0, Valkyrie.ComparisonType.GreaterThanOrEqual)]
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        [Required]
        public virtual Menu? Parent { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public virtual IPermission? Permissions { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [MaxLength(1024)]
        public string? Url { get; set; }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(MenuItem left, MenuItem right) => !(left == right);

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(MenuItem left, MenuItem right) => left is null ? right is null : left.CompareTo(right) < 0;

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(MenuItem left, MenuItem right) => left is null ? right is null : left.CompareTo(right) <= 0;

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(MenuItem first, MenuItem second)
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
        public static bool operator >(MenuItem left, MenuItem right) => left is null ? right is null : left.CompareTo(right) > 0;

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(MenuItem left, MenuItem right) => left is null ? right is null : left.CompareTo(right) >= 0;

        /// <summary>
        /// Determines whether this instance [can be viewed by] the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// <c>true</c> if this instance [can be viewed by] the specified user; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanBeViewedBy(ClaimsPrincipal? user) => (Permissions?.HasPermission(user) ?? true) && base.CanBeViewedBy(user);

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(MenuItem? other) => base.CompareTo(other);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(MenuItem? other) => base.Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => base.Equals(obj);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString() => Display ?? "";
    }
}