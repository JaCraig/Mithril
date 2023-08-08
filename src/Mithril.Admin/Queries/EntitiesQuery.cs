using Microsoft.Extensions.Logging;
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
    /// Entity listing query
    /// </summary>
    /// <seealso cref="QueryBaseClass&lt;ExpandoObject&gt;"/>
    public class EntitiesQuery : QueryBaseClass<List<ExpandoObject>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesQuery"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="editorService">The editor service.</param>
        public EntitiesQuery(ILogger<EntitiesQuery>? logger, IFeatureManager? featureManager, IEditorService? editorService) : base(logger, featureManager)
        {
            EditorService = editorService;
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public override IArgument[] Arguments { get; } = new IArgument[]
        {
            new Argument<string> { Description = "The type of entity to return", Name = "entityType" },
            new Argument<int> { Description = "Number of items to return", Name = "pageSize", DefaultValue = 10 },
            new Argument<int> { Description = "Page to return (starts at 0)", Name = "page", DefaultValue = 0 },
            new Argument<string> { Description = "Property to sort on", Name = "sortField", DefaultValue = "" },
            new Argument<bool> { Description = "If true, sort ascending. Otherwise sort descending.", Name = "sortAscending", DefaultValue = false },
            new Argument<string> { Description = "Value to filter the entities by", Name = "filter", DefaultValue = "" }
        };

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => "Entities";

        /// <summary>
        /// Gets the editor service.
        /// </summary>
        /// <value>The editor service.</value>
        private IEditorService? EditorService { get; }

        /// <summary>
        /// Used to resolve the data asked for by the query.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The data specified.</returns>
        public override async Task<List<ExpandoObject>?> ResolveAsync(ClaimsPrincipal? user, Arguments arguments)
        {
            var EntityType = arguments?.GetValue<string>("entityType") ?? "";
            var PageSize = arguments?.GetValue<int>("pageSize") ?? 0;
            var Page = arguments?.GetValue<int>("page") ?? 0;
            var SortField = arguments?.GetValue<string>("sortField") ?? "";
            var SortAscending = arguments?.GetValue<bool>("sortAscending") ?? false;
            var Filter = arguments?.GetValue<string>("filter") ?? "";
            IEntityEditor? EntityEditor = EditorService?.Editors.OfType<IEntityEditor>().FirstOrDefault(x => x.EntityType == EntityType);
            return EntityEditor?.CanView(user) != true
                ? new List<ExpandoObject>()
                : (await EntityEditor.LoadPageAsync(Page, PageSize, SortField, SortAscending, Filter, user).ConfigureAwait(false)).Select(x => x.ConvertToExpando() ?? new ExpandoObject()).ToList();
        }
    }
}