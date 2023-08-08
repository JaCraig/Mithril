using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Features.Models
{
    /// <summary>
    /// Feature model
    /// </summary>
    /// <seealso cref="ModelBase&lt;Feature&gt;"/>
    public class Feature : ModelBase<Feature>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Feature"/> class.
        /// </summary>
        public Feature()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Feature"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <exception cref="ArgumentNullException">name or category</exception>
        /// <exception cref="ArgumentException"></exception>
        public Feature(string name, string category)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (name.Length > 255)
                throw new ArgumentException($"{nameof(name)} has a max length of 255");
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException(nameof(category));
            if (category.Length > 255)
                throw new ArgumentException($"{nameof(category)} has a max length of 255");
            Name = name;
            Category = category;
        }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [MaxLength]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string? Name { get; set; }

        /// <summary>
        /// Loads the feature based on the name specified.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dataService">The data service.</param>
        /// <returns>The feature specified.</returns>
        public static Feature? Load(string name, IDataService? dataService)
        {
            return Query(dataService)?.Where(x => x.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Loads a specific feature or creates it.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="user">The user.</param>
        /// <returns>The feature specified.</returns>
        public static async Task<Feature> LoadOrCreateAsync(string name, string category, IDataService? dataService, ClaimsPrincipal? user)
        {
            var ReturnValue = Load(name, dataService);
            if (ReturnValue is null)
            {
                ReturnValue = new Feature(name, category);
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
        public static bool operator !=(Feature left, Feature right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Feature left, Feature right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(Feature left, Feature right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Feature first, Feature second)
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
        public static bool operator >(Feature left, Feature right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(Feature left, Feature right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(Feature? other)
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
        public bool Equals(Feature other)
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
    }
}