using Mithril.Core.Abstractions.Data.Interfaces;

namespace Mithril.Core.Abstractions.Security.Interfaces
{
    /// <summary>
    /// Tenant interface
    /// </summary>
    /// <seealso cref="IModel"/>
    public interface ITenant : IModel, IEquatable<ITenant>
    {
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        string? DisplayName { get; set; }
    }
}