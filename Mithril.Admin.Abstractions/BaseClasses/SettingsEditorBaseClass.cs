using Mithril.Admin.Abstractions.Components;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Services;
using System.Dynamic;
using System.Security.Claims;

namespace Mithril.Admin.Abstractions.BaseClasses
{
    /// <summary>
    /// Settings editor base class
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IEntityEditor&lt;TEntity&gt;" />
    public abstract class SettingsEditorBaseClass<TEntity, TModel> : EntityEditorBaseClass<TEntity, TModel>
        where TEntity : IEntity<TModel>, new()
        where TModel : ModelBase<TModel>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsEditorBaseClass{TEntity}" /> class.
        /// </summary>
        /// <param name="dataService"></param>
        /// <param name="dataType"></param>
        protected SettingsEditorBaseClass(IDataService dataService, string? dataType = null)
            : base(dataService, dataType ?? typeof(TEntity).Name)
        {
            if (string.IsNullOrEmpty(dataType))
                dataType = typeof(TEntity).Name;
            ComponentDefinition = new SettingsEditorComponent<TEntity>(dataType);
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public override string Category { get; } = "Settings";

        /// <summary>
        /// Activates the entity specified asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="currentUser"></param>
        /// <returns>
        /// True if it succeeds, false otherwise.
        /// </returns>
        public override Task<bool> ActivateAsync(long id, ClaimsPrincipal? currentUser)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Deletes the entity asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="currentUser"></param>
        /// <returns>
        /// True if it succeeds, false otherwise.
        /// </returns>
        public override Task<bool> DeleteEntityAsync(long id, ClaimsPrincipal? currentUser)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Loads the page asynchronously.
        /// </summary>
        /// <param name="page">The page (zero based).</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortAscending">if set to <c>true</c> [sort ascending].</param>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="currentUser"></param>
        /// <returns>
        /// The entities specified.
        /// </returns>
        public override Task<IEnumerable<IEntity>> LoadPageAsync(int page, int pageSize, string sortField, bool sortAscending, string searchQuery, ClaimsPrincipal? currentUser)
        {
            return Task.FromResult<IEnumerable<IEntity>>(new[] { Convert(LoadModel(0)) });
        }

        /// <summary>
        /// Loads the model.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The model requested
        /// </returns>
        protected override TModel LoadModel(long id)
        {
            return ModelBase<TModel>.Query(DataService)?.FirstOrDefault() ?? new TModel();
        }
    }

    /// <summary>
    /// Settings editor base class
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="EntityEditorBaseClass&lt;TEntity, TModel&gt;" />
    public abstract class SettingsEditorBaseClass<TEntity> : EntityEditorBaseClass<TEntity>
        where TEntity : IEntity, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsEditorBaseClass{TEntity}" /> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="dataType">Type of the data.</param>
        protected SettingsEditorBaseClass(IDataService dataService, string? dataType = null)
            : base(dataService, dataType ?? typeof(TEntity).Name)
        {
            if (string.IsNullOrEmpty(dataType))
                dataType = typeof(TEntity).Name;
            ComponentDefinition = new SettingsEditorComponent<TEntity>(dataType);
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public override string Category { get; } = "Settings";

        /// <summary>
        /// Activates the entity specified asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="currentUser"></param>
        /// <returns>
        /// True if it succeeds, false otherwise.
        /// </returns>
        public override Task<bool> ActivateAsync(long id, ClaimsPrincipal? currentUser)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Deletes the entity asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="currentUser"></param>
        /// <returns>
        /// True if it succeeds, false otherwise.
        /// </returns>
        public override Task<bool> DeleteEntityAsync(long id, ClaimsPrincipal? currentUser)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Loads the page asynchronously.
        /// </summary>
        /// <param name="page">The page (zero based).</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortAscending">if set to <c>true</c> [sort ascending].</param>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="currentUser"></param>
        /// <returns>
        /// The entities specified.
        /// </returns>
        public override Task<IEnumerable<IEntity>> LoadPageAsync(int page, int pageSize, string sortField, bool sortAscending, string searchQuery, ClaimsPrincipal? currentUser)
        {
            return Task.FromResult<IEnumerable<IEntity>>(new[] { Load(0, null, currentUser) ?? new TEntity() });
        }

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="currentUser"></param>
        /// <returns>
        /// True if it succeeds, false otherwise.
        /// </returns>
        public override Task<bool> SaveEntityAsync(long id, ExpandoObject entity, ClaimsPrincipal? currentUser)
        {
            return SaveEntityAsync(id, entity.ConvertExpando<TEntity>(), currentUser);
        }

        /// <summary>
        /// Gets the total active items.
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns>
        /// The total.
        /// </returns>
        public override Task<int> TotalActiveAsync(ClaimsPrincipal? currentUser) => Task.FromResult(1);

        /// <summary>
        /// Saves the entity asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns></returns>
        protected abstract Task<bool> SaveEntityAsync(long id, TEntity? entity, ClaimsPrincipal? currentUser);
    }
}