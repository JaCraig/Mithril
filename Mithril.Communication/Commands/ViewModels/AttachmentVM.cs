using System.ComponentModel.DataAnnotations;

namespace Mithril.Communication.Commands.ViewModels
{
    /// <summary>
    /// Attachment VM
    /// </summary>
    public class AttachmentVM
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        [MaxLength(128)]
        public string? FileName { get; set; }

        /// <summary>
        /// Gets or sets the location on disk.
        /// </summary>
        /// <value>The location on disk.</value>
        [MaxLength(1028)]
        public string? Location { get; set; }
    }
}