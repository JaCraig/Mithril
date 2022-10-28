using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Enums;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using System.ComponentModel.DataAnnotations;

namespace Mithril.Data.Models.General
{
    /// <summary>
    /// LookUpType object
    /// </summary>
    /// <seealso cref="ModelBase{LookUpType}"/>
    public class LookUpType : ModelBase<LookUpType>, IEquatable<LookUpType>, ILookUpType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpType"/> class.
        /// </summary>
        public LookUpType()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpType"/> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="description">The description.</param>
        /// <exception cref="ArgumentNullException">displayName</exception>
        /// <exception cref="ArgumentException">displayName or description</exception>
        public LookUpType(string displayName, string description)
        {
            if (string.IsNullOrEmpty(displayName))
                throw new ArgumentNullException(nameof(displayName));
            if (string.IsNullOrEmpty(description))
                description = "";
            if (displayName.Length > 64)
                throw new ArgumentException(nameof(displayName) + " must have a length less than or equal to 64");
            if (description.Length > 500)
                throw new ArgumentException(nameof(description) + " must have a length less than or equal to 500");
            DisplayName = displayName;
            Description = description;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [MaxLength(64)]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the look ups.
        /// </summary>
        /// <value>The look ups.</value>
        public virtual IList<ILookUp> LookUps { get; set; } = new List<ILookUp>();

        /// <summary>
        /// Loads the specified LookUpType based on the display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="dataService">The data service.</param>
        /// <returns>The LookUpType specified.</returns>
        public static LookUpType? Load(LookUpTypeEnum displayName, IDataService dataService)
        {
            return Query(dataService).Where(x => x.DisplayName == displayName).FirstOrDefault();
        }

        /// <summary>
        /// Loads the or create.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="description">The description.</param>
        /// <param name="dataService">The data service.</param>
        /// <returns>The LookUpType</returns>
        public static async Task<LookUpType> LoadOrCreateAsync(LookUpTypeEnum displayName, string description, IDataService dataService)
        {
            var Result = Load(displayName, dataService);
            if (Result is null)
            {
                Result = new LookUpType(displayName, description);
                await dataService.SaveAsync(Result).ConfigureAwait(false);
            }
            return Result;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(LookUpType? left, LookUpType? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(LookUpType? left, LookUpType? right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(LookUpType? left, LookUpType? right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(LookUpType? first, LookUpType? second)
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
        public static bool operator >(LookUpType? left, LookUpType? right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(LookUpType? left, LookUpType? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(LookUpType? other)
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
        public bool Equals(LookUpType? other)
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
        public bool Equals(ILookUpType? other)
        {
            return Equals(other as LookUpType);
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