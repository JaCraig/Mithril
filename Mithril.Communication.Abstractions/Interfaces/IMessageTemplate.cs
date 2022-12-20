using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.Communication.Abstractions.Interfaces
{
    /// <summary>
    /// Message template
    /// </summary>
    public interface IMessageTemplate : IModel
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        string? Content { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        string? DisplayName { get; set; }
    }
}