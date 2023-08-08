using System.Dynamic;

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
        /// Gets the schema.
        /// </summary>
        /// <value>
        /// The schema.
        /// </value>
        ExpandoObject? Schema { get; }

        /// <summary>
        /// Gets the script file.
        /// </summary>
        /// <value>
        /// The script file.
        /// </value>
        string ScriptFile { get; }
    }
}