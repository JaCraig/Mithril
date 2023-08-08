using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Themes.Models
{
    /// <summary>
    /// Theme model
    /// </summary>
    /// <seealso cref="ModelBase&lt;Theme&gt;"/>
    public class Theme : ModelBase<Theme>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Theme"/> class.
        /// </summary>
        public Theme()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Theme"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">name</exception>
        /// <exception cref="ArgumentException">name</exception>
        public Theme(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (name.Length > 64)
                throw new ArgumentException(nameof(name) + " is too long. Max of 64 characters allowed.");
            Name = name;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the input path.
        /// </summary>
        /// <value>The input path.</value>
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Loads the theme by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dataService">The data service.</param>
        /// <returns>The route specified.</returns>
        public static Theme? Load(string? name, IDataService? dataService)
        {
            if (string.IsNullOrEmpty(name) || dataService is null)
                return null;
            return Query(dataService)?.Where(x => x.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Loads or creates the route.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="user">The user.</param>
        /// <returns>The route entry.</returns>
        public static async Task<Theme> LoadOrCreateAsync(string name, IDataService? dataService, ClaimsPrincipal? user)
        {
            var ReturnValue = Load(name, dataService);
            if (ReturnValue is null)
            {
                ReturnValue = new Theme(name);
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
        public static bool operator !=(Theme left, Theme right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Theme left, Theme right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(Theme left, Theme right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Theme first, Theme second)
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
        public static bool operator >(Theme left, Theme right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(Theme left, Theme right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(Theme? other)
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
        public bool Equals(Theme other)
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
        public override string ToString() => Name ?? "";
    }
}