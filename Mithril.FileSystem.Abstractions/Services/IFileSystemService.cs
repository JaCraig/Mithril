using FileCurator;
using FileCurator.Interfaces;

namespace Mithril.FileSystem.Abstractions.Services
{
    /// <summary>
    /// File system service
    /// </summary>
    /// <seealso cref="IDisposable"/>
    public interface IFileSystemService : IDisposable
    {
        /// <summary>
        /// Gets the directory pointer to the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns>The directory</returns>
        IDirectory? Directory(string path, Credentials? credentials = null);

        /// <summary>
        /// Gets the file pointer to the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns>The file</returns>
        IFile? File(string path, Credentials? credentials = null);

        /// <summary>
        /// Translates the path from the file system to the URL
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The Url.</returns>
        Uri? GetUrl(string path);

        /// <summary>
        /// Translates the path from the file system to the URL
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The url.</returns>
        Uri? GetUrl(IFile file);
    }
}