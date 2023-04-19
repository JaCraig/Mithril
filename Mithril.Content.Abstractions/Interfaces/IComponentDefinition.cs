namespace Mithril.Content.Abstractions.Interfaces
{
    /// <summary>
    /// Component definition interface
    /// </summary>
    public interface IComponentDefinition
    {
        /// <summary>
        /// Gets the default properties.
        /// </summary>
        /// <value>The default properties.</value>
        Dictionary<string, string> DefaultProperties { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the definition.
        /// </summary>
        /// <returns>The component's definition.</returns>
        string GetDefinition();
    }
}