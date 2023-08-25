using BigBook;
using FileCurator.Interfaces;
using Mithril.FileSystem.Abstractions.Services;

namespace Mithril.FileSystem.ViewModels
{
    /// <summary>
    /// Directory VM
    /// TODO: Add tests
    /// </summary>
    public class FileBrowserDirectoryVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryVM"/> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="type">The type.</param>
        /// <param name="selectedFile">The selected file.</param>
        /// <param name="fileSystemService">The file system service.</param>
        public FileBrowserDirectoryVM(IDirectory directory, string type, IFile? selectedFile, IFileSystemService fileSystemService)
        {
            Type = type;
            SubDirectories = directory?.EnumerateDirectories()?.ForEach(x => new FileBrowserDirectoryVM(x, type, selectedFile, fileSystemService))?.ToArray() ?? Array.Empty<FileBrowserDirectoryVM>();
            Files = directory?.EnumerateFiles()?.ForEach(x => new MediaFileVM(x, type, fileSystemService))?.ToArray() ?? Array.Empty<MediaFileVM>();
            Name = directory?.Name ?? "";
            SelectedFile = selectedFile is null ? null : new MediaFileVM(selectedFile, type, fileSystemService);
        }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        public MediaFileVM[] Files { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the selected file.
        /// </summary>
        /// <value>The selected file.</value>
        public MediaFileVM? SelectedFile { get; }

        /// <summary>
        /// Gets or sets the sub directories.
        /// </summary>
        /// <value>The sub directories.</value>
        public FileBrowserDirectoryVM[] SubDirectories { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }
    }
}