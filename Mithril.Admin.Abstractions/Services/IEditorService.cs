using Mithril.Admin.Abstractions.Interfaces;

namespace Mithril.Admin.Abstractions.Services
{
    /// <summary>
    /// Editor service
    /// </summary>
    public interface IEditorService
    {
        /// <summary>
        /// Gets the editors.
        /// </summary>
        /// <value>
        /// The editors.
        /// </value>
        IEnumerable<IEditor> Editors { get; }
    }
}