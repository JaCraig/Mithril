using BigBook.ExtensionMethods;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Data.Abstractions.BaseClasses;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace Mithril.API.Abstractions.Commands.BaseClasses
{
    /// <summary>
    /// Event base class
    /// </summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    /// <seealso cref="ModelBase{TEvent}"/>
    /// <seealso cref="IEvent"/>
    public abstract class EventBaseClass<TEvent> : ModelBase<TEvent>, IEvent, IEquatable<EventBaseClass<TEvent>>
        where TEvent : EventBaseClass<TEvent>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventBaseClass{TEvent}"/> class.
        /// </summary>
        protected EventBaseClass()
        {
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; } = typeof(TEvent).Name.AddSpaces();

        /// <summary>
        /// Gets or sets the retry count.
        /// </summary>
        /// <value>The retry count.</value>
        public int RetryCount { get; set; } = 3;

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        [MaxLength(20)]
        public string? State { get; set; } = "Created";

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(EventBaseClass<TEvent> left, EventBaseClass<TEvent> right) => !(left == right);

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(EventBaseClass<TEvent> left, EventBaseClass<TEvent> right) => left is null ? right is null : left.CompareTo(right) < 0;

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(EventBaseClass<TEvent> left, EventBaseClass<TEvent> right) => left is null ? right is null : left.CompareTo(right) <= 0;

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(EventBaseClass<TEvent> first, EventBaseClass<TEvent> second)
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
        public static bool operator >(EventBaseClass<TEvent> left, EventBaseClass<TEvent> right) => left is null ? right is null : left.CompareTo(right) > 0;

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(EventBaseClass<TEvent> left, EventBaseClass<TEvent> right) => left is null ? right is null : left.CompareTo(right) >= 0;

        /// <summary>
        /// Determines whether this instance can run.
        /// </summary>
        /// <returns><c>true</c> if this instance can run; otherwise, <c>false</c>.</returns>
        public virtual bool CanRun() => true;

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public int CompareTo(EventBaseClass<TEvent>? other) => base.CompareTo(other as TEvent);

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
        public bool Equals(EventBaseClass<TEvent>? other) => CompareTo(other) == 0;

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(IEvent? other) => CompareTo(other) == 0;

        /// <summary>
        /// Gets the data within the event.
        /// </summary>
        /// <returns>The data from the event.</returns>
        public abstract ExpandoObject GetData();

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
        public abstract string GetSchema();

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString() => Name;
    }
}