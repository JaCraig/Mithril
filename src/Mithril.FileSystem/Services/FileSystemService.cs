using FileCurator;
using FileCurator.Interfaces;
using Mithril.FileSystem.Abstractions.Interfaces;
using Mithril.FileSystem.Abstractions.Services;

namespace Mithril.FileSystem.Services
{
    /// <summary>
    /// File system service
    /// </summary>
    /// <seealso cref="IFileSystemService"/>
    public class FileSystemService : IFileSystemService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemService"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="pathConverters">The path converters.</param>
        public FileSystemService(FileCurator.FileSystem? fileSystem, IEnumerable<IPathConverter> pathConverters)
        {
            FileSystem = fileSystem;
            PathConverters = pathConverters?.OrderBy(x => x.Order).ToArray() ?? Array.Empty<IPathConverter>();
        }

        /// <summary>
        /// Gets the file system.
        /// </summary>
        /// <value>The file system.</value>
        private FileCurator.FileSystem? FileSystem { get; }

        /// <summary>
        /// Gets the path converters.
        /// </summary>
        /// <value>The path converters.</value>
        private IPathConverter[] PathConverters { get; }

        /// <summary>
        /// Directories the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns>The directory</returns>
        public IDirectory? Directory(string path, Credentials? credentials = null) => FileSystem?.Directory(path, credentials);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Files the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns>The file</returns>
        public IFile? File(string path, Credentials? credentials = null) => FileSystem?.File(path, credentials);

        /// <summary>
        /// Translates the path from the file system to the URL
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The Url.</returns>
        public Uri? GetUrl(string path)
        {
            for (var x = 0; x < PathConverters.Length; ++x)
            {
                if (PathConverters[x].CanConvert(path))
                    return PathConverters[x].GetUrl(path);
            }
            return null;
        }

        /// <summary>
        /// Translates the path from the file system to the URL
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The url.</returns>
        public Uri? GetUrl(IFile file)
        {
            for (var x = 0; x < PathConverters.Length; ++x)
            {
                if (PathConverters[x].CanConvert(file))
                    return PathConverters[x].GetUrl(file);
            }
            return null;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="managed">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool managed) => FileSystem?.Dispose();
    }
}