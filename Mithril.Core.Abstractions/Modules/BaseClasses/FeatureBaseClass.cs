using Mithril.Core.Abstractions.Modules.Interfaces;

namespace Mithril.Core.Abstractions.Modules.BaseClasses
{
    /// <summary>
    /// Feature base class
    /// </summary>
    /// <typeparam name="TFeature">The type of the feature.</typeparam>
    /// <seealso cref="IFeature"/>
    /// <seealso cref="System.IEquatable{TFeature}"/>
    /// <seealso cref="System.IEquatable{FeatureBaseClass}"/>
    /// <seealso cref="IFeature"/>
    public abstract class FeatureBaseClass<TFeature> : IFeature, IEquatable<TFeature>
        where TFeature : FeatureBaseClass<TFeature>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureBaseClass{TFeature}"/> class.
        /// </summary>
        protected FeatureBaseClass()
        {
        }

        /// <summary>
        /// The group (by category) that the feature belongs.
        /// </summary>
        /// <value>The category.</value>
        public abstract string Category { get; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public abstract string Description { get; }

        /// <summary>
        /// Human-readable name of the feature.
        /// </summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="class1">The class1.</param>
        /// <param name="class2">The class2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(FeatureBaseClass<TFeature>? class1, FeatureBaseClass<TFeature>? class2)
        {
            return !(class1 == class2);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="class1">The class1.</param>
        /// <param name="class2">The class2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(FeatureBaseClass<TFeature>? class1, FeatureBaseClass<TFeature>? class2)
        {
            return EqualityComparer<FeatureBaseClass<TFeature>>.Default.Equals(class1, class2);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => Equals(obj as TFeature);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(TFeature? other) => string.Equals(Name, other?.Name, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(Name);
    }
}