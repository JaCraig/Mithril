using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using System.Security.Claims;

namespace Mithril.Admin.Abstractions.BaseClasses
{
    /// <summary>
    /// Entity base class
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IEntity&lt;TEntity&gt;"/>
    public abstract class EntityBaseClass<TEntity> : IEntity<TEntity>
        where TEntity : IModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBaseClass{TEntity}"/> class.
        /// </summary>
        protected EntityBaseClass()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBaseClass{TEntity}"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected EntityBaseClass(TEntity? entity)
        {
            if (entity is null)
                return;
            ID = entity.ID;
            Active = entity.Active;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IEntity"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        [Order(int.MaxValue)]
        public bool Active { get; set; } = true;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [ReadOnly]
        [DoNotList]
        public long ID { get; set; }

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>The async task.</returns>
        public abstract Task<TEntity?> SaveAsync(long id, IDataService? dataService, ClaimsPrincipal? currentUser);
    }
}