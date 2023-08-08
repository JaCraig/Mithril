using Mithril.Content.Abstractions.Interfaces;
using Mithril.Content.Abstractions.Services;

namespace Mithril.Content.Services
{
    /// <summary>
    /// Component service
    /// </summary>
    /// <seealso cref="IComponentService" />
    public class ComponentService : IComponentService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentService"/> class.
        /// </summary>
        /// <param name="componentDefinitions">The component definitions.</param>
        public ComponentService(IEnumerable<IComponentDefinition> componentDefinitions)
        {
            Components = componentDefinitions ?? Array.Empty<IComponentDefinition>();
        }

        /// <summary>
        /// Gets the components.
        /// </summary>
        /// <value>
        /// The components.
        /// </value>
        public IEnumerable<IComponentDefinition> Components { get; }

        /// <summary>
        /// Gets the component renderer.
        /// </summary>
        /// <param name="componentType">Type of the component.</param>
        /// <returns>
        /// The component renderer
        /// </returns>
        public IComponentDefinition? GetComponent(string componentType)
        {
            return Components.FirstOrDefault(x => string.Equals(x.Name, componentType, StringComparison.OrdinalIgnoreCase));
        }
    }
}