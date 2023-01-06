using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;

namespace Mithril.Data.Abstractions.BaseClasses
{
    /// <summary>
    /// Settings base class
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    /// <seealso cref="ModelBase&lt;TClass&gt;"/>
    /// <seealso cref="IEquatable&lt;SettingsBase&lt;TClass&gt;&gt;"/>
    public abstract class SettingsBase<TClass> : ModelBase<TClass>, IEquatable<SettingsBase<TClass>>
        where TClass : SettingsBase<TClass>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsBase{TClass}"/> class.
        /// </summary>
        protected SettingsBase()
        {
        }

        /// <summary>
        /// Loads or creates the settings asynchronously.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <returns>The settings</returns>
        public static async Task<TClass> LoadOrCreateAsync(IDataService? dataService)
        {
            var ReturnValue = dataService?.Query<TClass>()?.FirstOrDefault();
            if (ReturnValue is null)
            {
                ReturnValue = new TClass();
                if (dataService is not null)
                    await dataService.SaveAsync(ReturnValue).ConfigureAwait(false);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(SettingsBase<TClass>? left, SettingsBase<TClass>? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(SettingsBase<TClass>? left, SettingsBase<TClass>? right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(SettingsBase<TClass>? left, SettingsBase<TClass>? right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(SettingsBase<TClass>? first, SettingsBase<TClass>? second)
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
        public static bool operator >(SettingsBase<TClass>? left, SettingsBase<TClass>? right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(SettingsBase<TClass>? left, SettingsBase<TClass>? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(TClass? other)
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
        public bool Equals(ITypedModel? other)
        {
            return (other is SettingsBase<TClass> TempObject) && CompareTo(TempObject) == 0;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(SettingsBase<TClass>? other)
        {
            return other is not null && CompareTo(other) == 0;
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
            return (obj is SettingsBase<TClass> TempObject) && Equals(TempObject);
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