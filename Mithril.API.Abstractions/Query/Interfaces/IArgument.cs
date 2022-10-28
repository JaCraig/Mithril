namespace Mithril.API.Abstractions.Query.Interfaces
{
    /// <summary>
    /// Argument interface
    /// </summary>
    public interface IArgument
    {
        /// <summary>
        /// Gets the type of the argument.
        /// </summary>
        /// <value>The type of the argument.</value>
        Type ArgumentType { get; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
        object? DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        string? Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string? Name { get; set; }
    }
}