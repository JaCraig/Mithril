using BigBook;
using FileCurator.Interfaces;
using Mithril.FileSystem.Abstractions.Services;

namespace Mithril.FileSystem.ViewModels
{
    /// <summary>
    /// File browser directory VM
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="FileBrowserDirectoryVM"/> class.
    /// </remarks>
    /// <param name="directory">The directory.</param>
    /// <param name="type">The type.</param>
    /// <param name="selectedFile">The selected file.</param>
    /// <param name="fileSystemService">The file system service.</param>
    public class FileBrowserDirectoryVM(IDirectory? directory, string? type, IFile? selectedFile, IFileSystemService? fileSystemService)
    {
        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        public MediaFileVM[] Files { get; set; } = directory?.EnumerateFiles()?.ForEach(x => new MediaFileVM(x, type, fileSystemService))?.ToArray() ?? [];

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string? Name { get; set; } = directory?.Name ?? "";

        /// <summary>
        /// Gets the selected file.
        /// </summary>
        /// <value>The selected file.</value>
        public MediaFileVM? SelectedFile { get; } = selectedFile is null ? null : new MediaFileVM(selectedFile, type, fileSystemService);

        /// <summary>
        /// Gets or sets the sub directories.
        /// </summary>
        /// <value>The sub directories.</value>
        public FileBrowserDirectoryVM[] SubDirectories { get; set; } = directory?.EnumerateDirectories()?.ForEach(x => new FileBrowserDirectoryVM(x, type, selectedFile, fileSystemService))?.ToArray() ?? [];

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string? Type { get; set; } = type;
    }
}