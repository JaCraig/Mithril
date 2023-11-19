using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;

namespace Mithril.Admin.Services
{
    /// <summary>
    /// Editor service
    /// </summary>
    /// <seealso cref="IEditorService" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="EditorService"/> class.
    /// </remarks>
    /// <param name="editors">The editors.</param>
    public class EditorService(IEnumerable<IEditor> editors) : IEditorService
    {
        /// <summary>
        /// Gets the editors.
        /// </summary>
        /// <value>
        /// The editors.
        /// </value>
        public IEnumerable<IEditor> Editors { get; } = editors ?? Array.Empty<IEditor>();
    }
}