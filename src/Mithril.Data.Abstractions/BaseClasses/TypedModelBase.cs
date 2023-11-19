using Mithril.Data.Abstractions.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Mithril.Data.Abstractions.BaseClasses
{
    /// <summary>
    /// Typed model base
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    /// <seealso cref="BaseClasses.ModelBase{TClass}"/>
    public abstract class TypedModelBase<TClass> : ModelBase<TClass>, ITypedModel, IEquatable<TypedModelBase<TClass>>
        where TClass : TypedModelBase<TClass>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypedModelBase{TClass}"/> class.
        /// </summary>
        protected TypedModelBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedModelBase{TClass}"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        protected TypedModelBase(ILookUp type)
            : this(type?.DisplayName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedModelBase{TClass}"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="ArgumentNullException">type</exception>
        /// <exception cref="ArgumentOutOfRangeException">type</exception>
        protected TypedModelBase(string? type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException(nameof(type));
            if (type.Length > 64)
                throw new ArgumentOutOfRangeException(nameof(type), nameof(type) + " must have a length less than or equal to 64");
            Type = type;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [Required]
        [MaxLength(64)]
        public string? Type { get; set; }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(TypedModelBase<TClass>? left, TypedModelBase<TClass>? right) => !(left == right);

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(TypedModelBase<TClass>? left, TypedModelBase<TClass>? right) => left is null ? right is null : left.CompareTo(right) < 0;

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(TypedModelBase<TClass>? left, TypedModelBase<TClass>? right) => left is null ? right is null : left.CompareTo(right) <= 0;

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(TypedModelBase<TClass>? first, TypedModelBase<TClass>? second)
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
        public static bool operator >(TypedModelBase<TClass>? left, TypedModelBase<TClass>? right) => left is null ? right is null : left.CompareTo(right) > 0;

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(TypedModelBase<TClass>? left, TypedModelBase<TClass>? right) => left is null ? right is null : left.CompareTo(right) >= 0;

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(TClass? other) => base.CompareTo(other);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(ITypedModel? other) => (other is TypedModelBase<TClass> TempObject) && CompareTo(TempObject) == 0;

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(TypedModelBase<TClass>? other) => other is not null && CompareTo(other) == 0;

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => (obj is TypedModelBase<TClass> TempObject) && Equals(TempObject);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Determines if the object is of a specific type
        /// </summary>
        /// <param name="typeNames">Type name</param>
        /// <returns>True if it is, false otherwise</returns>
        public bool OfType(params string?[]? typeNames) => !string.IsNullOrEmpty(Type) && typeNames?.Contains(Type) == true;

        /// <summary>
        /// Determines if the object is of a specific type
        /// </summary>
        /// <param name="lookUps">The look ups.</param>
        /// <returns>True if it is, false otherwise</returns>
        public bool OfType(params ILookUp?[]? lookUps) => lookUps?.Any(x => x?.Equals(Type) ?? false) ?? false;
    }
}