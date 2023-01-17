using Mithril.Data.Abstractions.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace Mithril.Communication.Abstractions.Interfaces
{
    /// <summary>
    /// Message interface
    /// </summary>
    public interface IMessage : IModel
    {
        /// <summary>
        /// Gets or sets the application the message originated from.
        /// </summary>
        /// <value>The application the message originated from.</value>
        [MaxLength(64)]
        string? Application { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>The attachments.</value>
        IList<Attachment?> Attachments { get; set; }

        /// <summary>
        /// Gets or sets the BCC.
        /// </summary>
        /// <value>The BCC.</value>
        [MaxLength]
        string? BCC { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        [MaxLength]
        string? Body { get; set; }

        /// <summary>
        /// Gets or sets the cc.
        /// </summary>
        /// <value>The cc.</value>
        [MaxLength]
        string? CC { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        string? From { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(256)]
        string? Subject { get; set; }

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        [MaxLength(128)]
        string? Template { get; set; }

        /// <summary>
        /// Gets or sets the template data (JSON).
        /// </summary>
        /// <value>The template data in JSON format.</value>
        [MaxLength]
        string? TemplateData { get; set; }

        /// <summary>
        /// Gets or sets the template data.
        /// </summary>
        /// <value>The template data.</value>
        ExpandoObject? TemplateFields { get; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        [Required]
        [MinLength(1)]
        [MaxLength]
        string? To { get; set; }
    }
}