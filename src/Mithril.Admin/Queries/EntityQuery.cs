﻿using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using Mithril.API.Abstractions.Query;
using Mithril.API.Abstractions.Query.BaseClasses;
using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.Core.Abstractions.Extensions;
using System.Dynamic;
using System.Security.Claims;

namespace Mithril.Admin.Queries
{
    /// <summary>
    /// Entity query
    /// </summary>
    /// <seealso cref="QueryBaseClass&lt;IEntity&gt;"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EntityQuery"/> class.
    /// </remarks>
    /// <param name="logger">The logger.</param>
    /// <param name="featureManager">The feature manager.</param>
    /// <param name="editorService">The editor service.</param>
    public class EntityQuery(ILogger<EntityQuery>? logger, IFeatureManager? featureManager, IEditorService? editorService) : QueryBaseClass<ExpandoObject>(logger, featureManager)
    {
        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public override IArgument[] Arguments { get; } =
        [
            new Argument<string> { Description = "The type of entity to return", Name = "entityType" },
            new Argument<long> { Description = "The entity ID", Name = "id", DefaultValue = 0 }
        ];

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => "Entity";

        /// <summary>
        /// Gets the editor service.
        /// </summary>
        /// <value>The editor service.</value>
        private IEditorService? EditorService { get; } = editorService;

        /// <summary>
        /// Used to resolve the data asked for by the query.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The data specified.</returns>
        public override Task<ExpandoObject?> ResolveAsync(ClaimsPrincipal? user, Arguments arguments)
        {
            var EntityType = arguments?.GetValue<string>("entityType") ?? "";
            var ID = arguments?.GetValue<long>("id") ?? 0;
            IEntityEditor? EntityEditor = EditorService?.Editors.OfType<IEntityEditor>().FirstOrDefault(x => x.EntityType == EntityType);
            return EntityEditor?.CanView(user) != true
                ? Task.FromResult<ExpandoObject?>(null)
                : Task.FromResult(EntityEditor.Load(ID, null, user).ConvertToExpando());
        }
    }
}