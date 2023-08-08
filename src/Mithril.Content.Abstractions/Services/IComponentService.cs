using Mithril.Content.Abstractions.Interfaces;

namespace Mithril.Content.Abstractions.Services
{
    /// <summary>
    /// Component service
    /// </summary>
    public interface IComponentService
    {
        /// <summary>
        /// Gets the component renderer.
        /// </summary>
        /// <param name="componentType">Type of the component.</param>
        /// <returns>The component renderer</returns>
        IComponentDefinition? GetComponent(string componentType);

        /// <summary>
        /// Gets the components.
        /// </summary>
        /// <value>
        /// The components.
        /// </value>
        IEnumerable<IComponentDefinition> Components { get; }
    }
}