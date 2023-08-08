using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;

namespace Mithril.Admin.Services
{
    /// <summary>
    /// Editor service
    /// </summary>
    /// <seealso cref="IEditorService" />
    public class EditorService : IEditorService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorService"/> class.
        /// </summary>
        /// <param name="editors">The editors.</param>
        public EditorService(IEnumerable<IEditor> editors)
        {
            Editors = editors ?? Array.Empty<IEditor>();
        }

        /// <summary>
        /// Gets the editors.
        /// </summary>
        /// <value>
        /// The editors.
        /// </value>
        public IEnumerable<IEditor> Editors { get; }
    }
}