using Microsoft.Extensions.Hosting;
using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.Communication.Abstractions.Interfaces
{
    /// <summary>
    /// Message template
    /// </summary>
    public interface IMessageTemplate : IModel
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        string? DisplayName { get; set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <returns>The content</returns>
        string GetContent(IHostEnvironment? hostingEnvironment);

        /// <summary>
        /// Sets the content.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="content">The content.</param>
        IMessageTemplate SetContent(IHostEnvironment? hostingEnvironment, string content);
    }
}