using System.ComponentModel.DataAnnotations;

namespace Mithril.Data.Abstractions.Interfaces
{
    /// <summary>
    /// LookUpType interface
    /// </summary>
    /// <seealso cref="IEquatable&lt;ILookUpType&gt;"/>
    /// <seealso cref="IModel"/>
    public interface ILookUpType : IModel, IEquatable<ILookUpType>
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [MaxLength(500)]
        string? Description { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the look ups.
        /// </summary>
        /// <value>The look ups.</value>
        IList<ILookUp> LookUps { get; set; }
    }
}