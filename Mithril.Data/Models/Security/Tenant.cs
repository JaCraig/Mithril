using BigBook;
using Mithril.Core.Abstractions.Data.BaseClasses;
using Mithril.Core.Abstractions.Security.Interfaces;
using Mithril.Core.Abstractions.Services;
using System.ComponentModel.DataAnnotations;

namespace Mithril.Data.Models.Security
{
    /// <summary>
    /// Tenant model class
    /// </summary>
    /// <seealso cref="ITenant"/>
    /// <seealso cref="ModelBase&lt;Tenant&gt;"/>
    /// <seealso cref="IEquatable&lt;Tenant&gt;"/>
    public class Tenant : ModelBase<Tenant>, ITenant, IEquatable<Tenant>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tenant"/> class.
        /// </summary>
        public Tenant()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tenant"/> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <exception cref="ArgumentException">displayName</exception>
        public Tenant(string displayName)
        {
            if (!string.IsNullOrEmpty(displayName) && displayName.Length > 100)
                throw new ArgumentException(nameof(displayName) + " has a max length of 100 characters.");
            DisplayName = displayName;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Loads the specified tenant by display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="dataService">The data service.</param>
        /// <returns>The tenant specified.</returns>
        public static Tenant? Load(string displayName, IDataService dataService)
        {
            return Query(dataService).Where(x => x.DisplayName == displayName).FirstOrDefault();
        }

        /// <summary>
        /// Loads or creates the Tenant if necessary.
        /// </summary>
        /// <param name="TenantName">Name of the Tenant.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="context">The context.</param>
        /// <param name="claims">The claims.</param>
        /// <returns>The Tenant specified.</returns>
        public static async Task<Tenant> LoadOrCreateAsync(string displayName, IDataService context)
        {
            var ReturnValue = Load(displayName, context);
            if (ReturnValue is null)
            {
                ReturnValue = new Tenant(displayName);
                await context.SaveAsync(ReturnValue).ConfigureAwait(false);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Tenant? left, Tenant? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Tenant? left, Tenant? right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(Tenant? left, Tenant? right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Tenant? first, Tenant? second)
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
        public static bool operator >(Tenant? left, Tenant? right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(Tenant? left, Tenant? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(Tenant? other)
        {
            return other is null ? 1 : ID.CompareTo(other.ID);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(ITenant? other)
        {
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(Tenant? other)
        {
            return CompareTo(other) == 0;
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
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(DisplayName) ? "New Tenant" : DisplayName;
        }
    }
}