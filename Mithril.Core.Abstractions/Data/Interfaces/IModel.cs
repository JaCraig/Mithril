using Mithril.Core.Abstractions.Security.Interfaces;
using Valkyrie;

namespace Mithril.Core.Abstractions.Data.Interfaces
{
    /// <summary>
    /// IModel interface
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    public interface IModel<TClass> : IModel, IEquatable<IModel<TClass>>
        where TClass : IModel<TClass>, new()
    {
        /// <summary>
        /// Setups the object.
        /// </summary>
        TClass SetupObjectAndReturn();
    }

    /// <summary>
    /// Model interface
    /// </summary>
    public interface IModel : IEquatable<IModel>
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IModel"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        bool Active { get; set; }

        /// <summary>
        /// Gets or sets the creator.
        /// </summary>
        /// <value>The creator.</value>
        IUser? Creator { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        [Between("1/1/1900", "1/1/2100")]
        DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        [Between("1/1/1900", "1/1/2100")]
        DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        long ID { get; set; }

        /// <summary>
        /// Gets or sets the modifier.
        /// </summary>
        /// <value>The modifier.</value>
        IUser? Modifier { get; set; }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <param name="softDelete">if set to <c>true</c> [soft delete].</param>
        void Delete(bool softDelete = true);

        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();

        /// <summary>
        /// Saves this instance asynchronously.
        /// </summary>
        /// <returns>The async task.</returns>
        Task SaveAsync();

        /// <summary>
        /// Setups the object.
        /// </summary>
        void SetupObject();
    }
}