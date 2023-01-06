using System.ComponentModel.DataAnnotations;

namespace Mithril.Communication.Commands.ViewModels
{
    /// <summary>
    /// Send message command VM
    /// </summary>
    public class SendMessageCommandVM
    {
        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>The attachments.</value>
        public List<AttachmentVM> Attachments { get; set; } = new List<AttachmentVM>();

        /// <summary>
        /// Gets or sets the BCC.
        /// </summary>
        /// <value>The BCC.</value>
        public string? BCC { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public string? Body { get; set; }

        /// <summary>
        /// Gets or sets the cc.
        /// </summary>
        /// <value>The cc.</value>
        public string? CC { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        public string? Channel { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        public string? From { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(256)]
        public string? Subject { get; set; }

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        public string? Template { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public string? To { get; set; }
    }
}