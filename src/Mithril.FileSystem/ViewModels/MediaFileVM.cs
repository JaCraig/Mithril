using FileCurator.Interfaces;
using Mithril.FileSystem.Abstractions.Services;

namespace Mithril.FileSystem.ViewModels
{
    /// <summary>
    /// Media file VM
    /// TODO: Add tests
    /// </summary>
    public class MediaFileVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFileVM"/> class.
        /// </summary>
        public MediaFileVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFileVM"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="type">The type.</param>
        /// <param name="fileSystemService">The file system service.</param>
        public MediaFileVM(IFile file, string type, IFileSystemService fileSystemService)
        {
            if (file is null || fileSystemService is null)
                return;
            Name = file.Name.Replace(file.Extension, "", StringComparison.OrdinalIgnoreCase);
            URL = fileSystemService.GetUrl(file.FullName)?.AbsolutePath ?? "";
            //DeleteURL = fileSystemService.GetDeleteUrl(file.FullName)?.ToString() ?? "";
            ImageURL = GetImageUrl(type);
            Icon = GetIcon(file, type);
            DateModified = file.Modified;
        }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the delete URL.
        /// </summary>
        /// <value>The delete URL.</value>
        public string? DeleteURL { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>The image URL.</value>
        public string? ImageURL { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string? URL { get; set; }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="type">The type.</param>
        /// <returns>The icon to use</returns>
        private static string GetIcon(IFile file, string type)
        {
            if (string.Equals(type, "IMAGE", StringComparison.OrdinalIgnoreCase))
                return "";
            if (string.Equals(file.Extension, ".PDF", StringComparison.OrdinalIgnoreCase))
                return "fa-file-pdf";
            if (string.Equals(file.Extension, ".DOCX", StringComparison.OrdinalIgnoreCase))
                return "fa-file-word";
            return string.Equals(file.Extension, ".PPTX", StringComparison.OrdinalIgnoreCase)
                ? "fa-file-powerpoint"
                : string.Equals(type, "MEDIA", StringComparison.OrdinalIgnoreCase) ? "fa-file-video" : "fa-file-alt";
        }

        /// <summary>
        /// Gets the image URL.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The image url</returns>
        private string GetImageUrl(string type) => string.Equals(type, "IMAGE", StringComparison.OrdinalIgnoreCase) ? URL ?? "" : "";
    }
}