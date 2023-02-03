using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.Routing.Abstractions.Interfaces
{
    /// <summary>
    /// Route interface
    /// </summary>
    public interface IRoute : IModel
    {
        /// <summary>
        /// Gets or sets the input path.
        /// </summary>
        /// <value>The input path.</value>
        string? InputPath { get; set; }

        /// <summary>
        /// Gets or sets the output path.
        /// </summary>
        /// <value>The output path.</value>
        string? OutputPath { get; set; }
    }
}