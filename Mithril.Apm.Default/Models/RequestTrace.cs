using Mithril.Data.Abstractions.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace Mithril.Apm.Default.Models
{
    /// <summary>
    /// Request trace
    /// </summary>
    /// <seealso cref="ModelBase&lt;RequestTrace&gt;"/>
    public class RequestTrace : ModelBase<RequestTrace>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTrace"/> class.
        /// </summary>
        public RequestTrace()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTrace"/> class.
        /// </summary>
        /// <param name="traceIdentifier">The trace identifier.</param>
        /// <exception cref="System.ArgumentNullException">traceIdentifier</exception>
        public RequestTrace(string traceIdentifier)
        {
            if (string.IsNullOrEmpty(traceIdentifier))
                throw new ArgumentNullException(nameof(traceIdentifier));
            TraceIdentifier = traceIdentifier;
        }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        /// <value>The meta data.</value>
        public virtual IList<RequestMetaData> MetaData { get; set; } = new List<RequestMetaData>();

        /// <summary>
        /// Gets or sets the metrics.
        /// </summary>
        /// <value>The metrics.</value>
        public virtual IList<RequestMetric> Metrics { get; set; } = new List<RequestMetric>();

        /// <summary>
        /// Gets or sets the trace identifier.
        /// </summary>
        /// <value>The trace identifier.</value>
        [Required]
        [MaxLength(100)]
        public string? TraceIdentifier { get; set; }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(RequestTrace left, RequestTrace right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(RequestTrace left, RequestTrace right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(RequestTrace left, RequestTrace right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(RequestTrace first, RequestTrace second)
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
        public static bool operator >(RequestTrace left, RequestTrace right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(RequestTrace left, RequestTrace right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(RequestTrace? other)
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
        public bool Equals(RequestTrace other)
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
            return TraceIdentifier ?? "";
        }
    }
}