using Mithril.Data.Abstractions.Interfaces;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        [MaxLength(1024)]
        string? InputPath { get; set; }

        /// <summary>
        /// Gets or sets the output path.
        /// </summary>
        /// <value>The output path.</value>
        [Required]
        [MaxLength(1024)]
        string? OutputPath { get; set; }
    }
}