using Mithril.API.Abstractions.Commands.BaseClasses;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace Mithril.Features.Models
{
    /// <summary>
    /// Feature toggled event
    /// </summary>
    /// <seealso cref="EventBaseClass&lt;FeatureToggledEvent&gt;"/>
    public class FeatureToggledEvent : EventBaseClass<FeatureToggledEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggledEvent"/> class.
        /// </summary>
        public FeatureToggledEvent()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggledEvent"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        public FeatureToggledEvent(string name, bool active)
        {
            FeatureName = name;
            FeatureStatus = active;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [MaxLength(100)]
        public string? FeatureName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [feature status].
        /// </summary>
        /// <value><c>true</c> if [feature status]; otherwise, <c>false</c>.</value>
        public bool FeatureStatus { get; set; }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(FeatureToggledEvent left, FeatureToggledEvent right) => !(left == right);

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(FeatureToggledEvent left, FeatureToggledEvent right) => left is null ? right is null : left.CompareTo(right) < 0;

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(FeatureToggledEvent left, FeatureToggledEvent right) => left is null ? right is null : left.CompareTo(right) <= 0;

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(FeatureToggledEvent first, FeatureToggledEvent second) => ReferenceEquals(first, second) || (first is not null && second is not null && first.CompareTo(second) == 0);

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(FeatureToggledEvent left, FeatureToggledEvent right) => left is null ? right is null : left.CompareTo(right) > 0;

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(FeatureToggledEvent left, FeatureToggledEvent right) => left is null ? right is null : left.CompareTo(right) >= 0;

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(FeatureToggledEvent? other) => base.CompareTo(other);

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => base.Equals(obj);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(FeatureToggledEvent other) => base.Equals(other);

        /// <summary>
        /// Gets the data within the event.
        /// </summary>
        /// <returns>The data from the event.</returns>
        public override ExpandoObject GetData() => new();

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <returns>The data schema.</returns>
        public override string GetSchema() => "";
    }
}