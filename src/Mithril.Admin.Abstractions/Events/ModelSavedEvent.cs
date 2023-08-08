using Mithril.API.Abstractions.Commands.BaseClasses;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace Mithril.Admin.Abstractions.Events
{
    /// <summary>
    /// Model saved event
    /// </summary>
    /// <seealso cref="EventBaseClass&lt;ModelSavedEvent&gt;"/>
    public class ModelSavedEvent : EventBaseClass<ModelSavedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelSavedEvent"/> class.
        /// </summary>
        public ModelSavedEvent()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelSavedEvent"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentException">entityType - entityType</exception>
        public ModelSavedEvent(ExpandoObject? data, string? entityType, long id)
            : this(System.Text.Json.JsonSerializer.Serialize(data ?? new ExpandoObject()), entityType, id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelSavedEvent"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentException">entityType - entityType</exception>
        public ModelSavedEvent(string? data, string? entityType, long id)
        {
            if ((entityType?.Length ?? 0) > 64)
                throw new ArgumentException(nameof(entityType) + " cannot be longer than 64 characters.", nameof(entityType));
            Data = data ?? "";
            EntityType = entityType;
            EntityID = id;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        [MaxLength]
        public string? Data { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long EntityID { get; set; }

        /// <summary>
        /// Gets or sets the type of the entity.
        /// </summary>
        /// <value>The type of the entity.</value>
        [MaxLength(64)]
        public string? EntityType { get; set; }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ModelSavedEvent left, ModelSavedEvent right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(ModelSavedEvent left, ModelSavedEvent right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(ModelSavedEvent left, ModelSavedEvent right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ModelSavedEvent first, ModelSavedEvent second)
        {
            return ReferenceEquals(first, second) || (first is not null && second is not null && first.CompareTo(second) == 0);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(ModelSavedEvent left, ModelSavedEvent right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(ModelSavedEvent left, ModelSavedEvent right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(ModelSavedEvent? other) => base.CompareTo(other);

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
        public bool Equals(ModelSavedEvent other) => base.Equals(other);

        /// <summary>
        /// Gets the data within the event.
        /// </summary>
        /// <returns>The data from the event.</returns>
        public override ExpandoObject GetData()
        {
            try
            {
                return string.IsNullOrEmpty(Data)
                    ? new ExpandoObject()
                    : System.Text.Json.JsonSerializer.Deserialize<ExpandoObject>(Data ?? "{}") ?? new ExpandoObject();
            }
            catch (Exception)
            {
                return new ExpandoObject();
            }
        }

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