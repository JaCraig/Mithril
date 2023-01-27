using Mithril.Data.Abstractions.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace Mithril.Apm.Default.Models
{
    /// <summary>
    /// Request Metrics
    /// </summary>
    /// <seealso cref="ModelBase&lt;RequestMetrics&gt;"/>
    public class RequestMetric : ModelBase<RequestMetric>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMetric"/> class.
        /// </summary>
        public RequestMetric()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMetric"/> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="metaData">The meta data.</param>
        /// <param name="metric">The metric.</param>
        /// <exception cref="ArgumentNullException">displayName</exception>
        public RequestMetric(string displayName, string metaData, decimal metric)
        {
            if (string.IsNullOrEmpty(displayName))
                throw new ArgumentNullException(nameof(displayName));
            DisplayName = displayName;
            MetaData = metaData;
            Metric = metric;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [Required]
        [MaxLength(100)]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        /// <value>The meta data.</value>
        [MaxLength]
        public string? MetaData { get; set; }

        /// <summary>
        /// Gets or sets the metric.
        /// </summary>
        /// <value>The metric.</value>
        public decimal Metric { get; set; }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(RequestMetric left, RequestMetric right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(RequestMetric left, RequestMetric right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(RequestMetric left, RequestMetric right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(RequestMetric first, RequestMetric second)
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
        public static bool operator >(RequestMetric left, RequestMetric right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(RequestMetric left, RequestMetric right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(RequestMetric? other)
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
        public bool Equals(RequestMetric other)
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
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return (DisplayName ?? "") + ": " + Metric;
        }
    }
}