using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.Admin.Abstractions.Services;
using Mithril.Admin.Queries.ViewModels;
using Mithril.API.Abstractions.Query;
using Mithril.API.Abstractions.Query.BaseClasses;
using System.Security.Claims;

namespace Mithril.Admin.Queries
{
    /// <summary>
    /// Editor listing query
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="QueryBaseClass&lt;IEnumerable&lt;EditorVM&gt;&gt;" />
    public class EditorListingQuery : QueryBaseClass<IEnumerable<EditorVM>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorListingQuery"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="editorService">The editor service.</param>
        public EditorListingQuery(ILogger<EditorListingQuery>? logger, IFeatureManager? featureManager, IEditorService editorService) : base(logger, featureManager)
        {
            EditorService = editorService;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string Name => "Editors";

        /// <summary>
        /// Gets the editor service.
        /// </summary>
        /// <value>
        /// The editor service.
        /// </value>
        private IEditorService EditorService { get; }

        /// <summary>
        /// Used to resolve the data asked for by the query.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        /// The data specified.
        /// </returns>
        public override Task<IEnumerable<EditorVM>?> ResolveAsync(ClaimsPrincipal? user, Arguments arguments)
        {
            return Task.FromResult<IEnumerable<EditorVM>?>(EditorService.Editors.Where(x => x.CanView(user)).Select(x => new EditorVM(x)));
        }
    }
}