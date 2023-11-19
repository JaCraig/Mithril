using Mithril.Content.Abstractions.Interfaces;
using Mithril.Content.Abstractions.Services;

namespace Mithril.Content.Services
{
    /// <summary>
    /// Component service
    /// </summary>
    /// <seealso cref="IComponentService" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="ComponentService"/> class.
    /// </remarks>
    /// <param name="componentDefinitions">The component definitions.</param>
    public class ComponentService(IEnumerable<IComponentDefinition> componentDefinitions) : IComponentService
    {
        /// <summary>
        /// Gets the components.
        /// </summary>
        /// <value>
        /// The components.
        /// </value>
        public IEnumerable<IComponentDefinition> Components { get; } = componentDefinitions ?? Array.Empty<IComponentDefinition>();

        /// <summary>
        /// Gets the component renderer.
        /// </summary>
        /// <param name="componentType">Type of the component.</param>
        /// <returns>
        /// The component renderer
        /// </returns>
        public IComponentDefinition? GetComponent(string componentType) => Components.FirstOrDefault(x => string.Equals(x.Name, componentType, StringComparison.OrdinalIgnoreCase));
    }
}