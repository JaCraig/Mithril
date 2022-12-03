using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Enums;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using System.ComponentModel.DataAnnotations;

namespace Mithril.Data.Models.General
{
    /// <summary>
    /// LookUp Class
    /// </summary>
    /// <seealso cref="ModelBase{LookUp}"/>
    public class LookUp : ModelBase<LookUp>, IEquatable<LookUp>, ILookUp
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUp"/> class.
        /// </summary>
        public LookUp()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LookUp"/> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="type">The type.</param>
        /// <exception cref="ArgumentNullException">displayName or icon or type</exception>
        /// <exception cref="ArgumentException">displayName or icon</exception>
        public LookUp(string displayName, string icon, ILookUpType type)
        {
            if (string.IsNullOrEmpty(displayName))
                throw new ArgumentNullException(nameof(displayName));
            if (string.IsNullOrEmpty(icon))
                throw new ArgumentNullException(nameof(icon));
            if (displayName.Length > 64)
                throw new ArgumentException(nameof(displayName) + " must have a length less than or equal to 64");
            if (icon.Length > 64)
                throw new ArgumentException(nameof(icon) + " must have a length less than or equal to 64");
            DisplayName = displayName.Trim();
            Icon = icon.Trim();
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Display name
        /// </summary>
        [MaxLength(64)]
        [Required]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        [MaxLength(64)]
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public virtual ILookUpType? Type { get; set; }

        /// <summary>
        /// Loads the specified LookUp based on the display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="type">The type.</param>
        /// <param name="context">The context.</param>
        /// <returns>The lookup specified.</returns>
        public static ILookUp? Load(string displayName, LookUpTypeEnum type, IDataService? context)
        {
            return LookUpType.Load(type, context)?.LookUps.FirstOrDefault(x => x.DisplayName == displayName);
        }

        /// <summary>
        /// Loads the LookUp based on the display name and look up type specified or creates it if
        /// it doesn't exist.
        /// </summary>
        /// <param name="displayName">Display name specified</param>
        /// <param name="type">Lookup type specified</param>
        /// <param name="icon">The icon.</param>
        /// <param name="context">The context.</param>
        /// <returns>LookUp associated with the display name</returns>
        public static async Task<ILookUp> LoadOrCreateAsync(string displayName, LookUpTypeEnum type, string icon, IDataService? context)
        {
            var Result = Load(displayName, type, context);
            if (Result is null)
            {
                var TempType = await LookUpType.LoadOrCreateAsync(type, "", context).ConfigureAwait(false);
                Result = new LookUp(displayName, icon, TempType);
                if (context is not null)
                    await context.SaveAsync(Result).ConfigureAwait(false);
            }
            return Result;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(LookUp? left, LookUp? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(LookUp? left, LookUp? right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(LookUp? left, LookUp? right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(LookUp? first, LookUp? second)
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
        public static bool operator >(LookUp? left, LookUp? right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(LookUp? left, LookUp? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(LookUp? other)
        {
            return base.CompareTo(other);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(LookUp? other)
        {
            return base.Equals(other);
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
        /// <see langword="true"/> if the current object is equal to the <paramref name="other"/>
        /// parameter; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(ILookUp? other)
        {
            return Equals(other as LookUp);
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
            return DisplayName ?? "";
        }
    }
}