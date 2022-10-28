using System.ComponentModel.DataAnnotations;

namespace Mithril.Data.Abstractions.Interfaces
{
    /// <summary>
    /// LookUp interface
    /// </summary>
    /// <seealso cref="IModel"/>
    /// <seealso cref="IEquatable&lt;ILookUp&gt;"/>
    public interface ILookUp : IModel, IEquatable<ILookUp>
    {
        /// <summary>
        /// Display name
        /// </summary>
        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        ILookUpType? Type { get; set; }
    }
}