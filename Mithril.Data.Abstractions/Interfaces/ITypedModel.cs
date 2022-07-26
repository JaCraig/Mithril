﻿using System.ComponentModel.DataAnnotations;

namespace Mithril.Data.Abstractions.Interfaces
{
    /// <summary>
    /// Typed model
    /// </summary>
    public interface ITypedModel : IModel, IEquatable<ITypedModel>
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [Required]
        [MaxLength(64)]
        [MinLength(1)]
        string? Type { get; set; }

        /// <summary>
        /// Determines if the object is of a specific type
        /// </summary>
        /// <param name="typeNames">Type name</param>
        /// <returns>True if it is, false otherwise</returns>
        bool OfType(params string?[]? typeNames);
    }
}