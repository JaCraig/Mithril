﻿using BigBook;
using Mithril.Admin.Abstractions.Components;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Content.Abstractions.Interfaces;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Enums;
using Mithril.Data.Abstractions.Services;
using System.Dynamic;
using System.Security.Claims;

namespace Mithril.Admin.Abstractions.BaseClasses
{
    /// <summary>
    /// Entity editor base class
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="IEntityEditor&lt;TEntity&gt;" />
    public abstract class EntityEditorBaseClass<TEntity, TModel> : EntityEditorBaseClass<TEntity>
        where TEntity : IEntity<TModel>, new()
        where TModel : ModelBase<TModel>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityEditorBaseClass{TEntity}"/> class.
        /// </summary>
        protected EntityEditorBaseClass(IDataService dataService, string? dataType = null)
            : base(dataService, dataType ?? typeof(TEntity).Name)
        {
        }

        /// <summary>
        /// Activates the entity specified asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// True if it succeeds, false otherwise.
        /// </returns>
        public override async Task<bool> ActivateAsync(long id, ClaimsPrincipal? currentUser)
        {
            var TempClaim = LoadModel(id);
            if (TempClaim is null)
                return false;
            if (TempClaim.Active)
                await MakeActiveAsync(TempClaim).ConfigureAwait(false);
            TempClaim.Active = true;
            await TempClaim.SaveAsync(DataService, currentUser).ConfigureAwait(false);
            //if (TempClaim is IIndexedModel indexedModel)
            //    await IndexService.IndexAsync(indexedModel).ConfigureAwait(false);
            return true;
        }

        /// <summary>
        /// Determines whether this instance can be viewed by the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>true</c> if this instance can be viewed by the specified user; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanView(ClaimsPrincipal user)
        {
            return user.HasClaim(UserClaimTypes.Role, "Admin");
        }

        /// <summary>
        /// Deletes the entity asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// True if it succeeds, false otherwise.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override async Task<bool> DeleteEntityAsync(long id, ClaimsPrincipal? currentUser)
        {
            var Model = LoadModel(id);
            if (Model is null)
                return false;
            if (Model.Active)
                await MakeInactiveAsync(Model).ConfigureAwait(false);
            await Model.DeleteAsync(DataService, currentUser, Model.Active).ConfigureAwait(false);
            //if (Model is IIndexedModel indexedModel)
            //    await IndexService.RemoveAsync(indexedModel).ConfigureAwait(false);
            return true;
        }

        /// <summary>
        /// Loads the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public override IEntity? Load(long id, ExpandoObject? model, ClaimsPrincipal? currentUser)
        {
            return model is null ? Convert(LoadModel(id)) : model.ConvertExpando<TEntity>();
        }

        /// <summary>
        /// Loads the page asynchronously.
        /// </summary>
        /// <param name="page">The page (zero based).</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortAscending">if set to <c>true</c> [sort ascending].</param>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>
        /// The entities specified.
        /// </returns>
        public override Task<IEnumerable<IEntity>> LoadPageAsync(int page, int pageSize, string sortField, bool sortAscending, string searchQuery, ClaimsPrincipal? currentUser)
        {
            IQueryable<TModel>? Query = ModelBase<TModel>.Query(DataService);
            if (!string.IsNullOrEmpty(sortField))
            {
                sortField = sortField.ToPascalCase();
                var SortProperty = typeof(TModel).GetProperty(sortField, true);
                if (SortProperty is not null)
                {
                    if (sortAscending)
                        Query = Query?.OrderBy(SortProperty.PropertyGetter<TModel, string>());
                    else
                        Query = Query?.OrderByDescending(SortProperty.PropertyGetter<TModel, string>());
                }
            }
            Query = FilterQueryBySearchQuery(Query, searchQuery);
            return Task.FromResult(Query?.Skip(page * pageSize).Take(pageSize).ToList().ConvertAll(x => Convert(x, false)) ?? (IEnumerable<IEntity>)Array.Empty<IEntity>());
        }

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// True if it succeeds, false otherwise.
        /// </returns>
        public override async Task<bool> SaveEntityAsync(long id, ExpandoObject entity, ClaimsPrincipal? currentUser)
        {
            if (entity is null)
                return false;
            var ModelValue = entity.ConvertExpando<TEntity>();
            if (ModelValue is null)
                return false;
            var Model = await ModelValue.SaveAsync(id, DataService).ConfigureAwait(false);
            if (Model is null) return false;
            //if (Model is IIndexedModel indexedModel)
            //{
            //    if (!Model.Active)
            //        await IndexService.RemoveAsync(indexedModel).ConfigureAwait(false);
            //    else
            //        await IndexService.IndexAsync(indexedModel).ConfigureAwait(false);
            //}
            return true;
        }

        /// <summary>
        /// Gets the total active items.
        /// </summary>
        /// <returns>
        /// The total.
        /// </returns>
        public override Task<int> TotalActiveAsync(ClaimsPrincipal? currentUser)
        {
            return Task.FromResult(DataService.Query<TModel>()?.Where(x => x.Active).Count() ?? 0);
        }

        /// <summary>
        /// Converts the model into the appropriate view model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        /// <returns>The view model</returns>
        protected abstract IEntity Convert(TModel model, bool full = true);

        /// <summary>
        /// Filters the query by search term.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>The resulting query.</returns>
        protected virtual IQueryable<TModel>? FilterQueryBySearchQuery(IQueryable<TModel>? query, string searchQuery) => query;

        /// <summary>
        /// Loads the model.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The model requested</returns>
        protected virtual TModel LoadModel(long id)
        {
            return ModelBase<TModel>.Load(id, DataService) ?? new TModel();
        }

        /// <summary>
        /// Makes the model active.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The async task.</returns>
        protected virtual Task MakeActiveAsync(TModel model) => Task.CompletedTask;

        /// <summary>
        /// Makes the model inactive.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The async task.</returns>
        protected virtual Task MakeInactiveAsync(TModel model) => Task.CompletedTask;
    }

    /// <summary>
    /// Entity editor base class
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IEntityEditor&lt;TEntity&gt;" />
    public abstract class EntityEditorBaseClass<TEntity> : IEntityEditor<TEntity>
        where TEntity : IEntity, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityEditorBaseClass{TEntity}" /> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="dataType">Type of the data.</param>
        protected EntityEditorBaseClass(IDataService dataService, string? dataType = null)
        {
            if (string.IsNullOrEmpty(dataType))
                dataType = typeof(TEntity).Name;
            ComponentDefinition = new DataEditorComponent<TEntity>(dataType);
            DataService = dataService;
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public virtual string Category { get; } = typeof(TEntity).Namespace?.Split('.')[1] ?? "";

        /// <summary>
        /// Gets the component definition.
        /// </summary>
        /// <value>
        /// The component definition.
        /// </value>
        public IComponentDefinition ComponentDefinition { get; protected set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public virtual string Description { get; } = typeof(TEntity).Name.Replace("VM", "").AddSpaces().Replace("-", " ", StringComparison.Ordinal) + " Editor";

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public virtual string Icon { get; } = "fas fa-square-pen";

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get; } = typeof(TEntity).Name.Replace("VM", "").AddSpaces().Replace("-", " ", StringComparison.Ordinal);

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>
        /// The data service.
        /// </value>
        protected IDataService DataService { get; }

        /// <summary>
        /// Activates the entity specified asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// True if it succeeds, false otherwise.
        /// </returns>
        public abstract Task<bool> ActivateAsync(long id, ClaimsPrincipal? currentUser);

        /// <summary>
        /// Determines whether this instance can be viewed by the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>true</c> if this instance can be viewed by the specified user; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanView(ClaimsPrincipal user)
        {
            return user.HasClaim(UserClaimTypes.Role, "Admin");
        }

        /// <summary>
        /// Deletes the entity asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// True if it succeeds, false otherwise.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public abstract Task<bool> DeleteEntityAsync(long id, ClaimsPrincipal? currentUser);

        /// <summary>
        /// Loads the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>The entity specified.</returns>
        public abstract IEntity? Load(long id, ExpandoObject? model, ClaimsPrincipal? currentUser);

        /// <summary>
        /// Loads the page asynchronously.
        /// </summary>
        /// <param name="page">The page (zero based).</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortAscending">if set to <c>true</c> [sort ascending].</param>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>
        /// The entities specified.
        /// </returns>
        public abstract Task<IEnumerable<IEntity>> LoadPageAsync(int page, int pageSize, string sortField, bool sortAscending, string searchQuery, ClaimsPrincipal? currentUser);

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// True if it succeeds, false otherwise.
        /// </returns>
        public abstract Task<bool> SaveEntityAsync(long id, ExpandoObject entity, ClaimsPrincipal? currentUser);

        /// <summary>
        /// Gets the total active items.
        /// </summary>
        /// <returns>
        /// The total.
        /// </returns>
        public abstract Task<int> TotalActiveAsync(ClaimsPrincipal? currentUser);
    }
}