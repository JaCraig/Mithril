using FileCurator;
using FileCurator.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Mithril.FileSystem.Abstractions.BaseClasses;

namespace Mithril.FileSystem.LocalFileSystem
{
    /// <summary>
    /// Local file system
    /// </summary>
    /// <seealso cref="MediaFileSystemBaseClass"/>
    public class LocalSystem : MediaFileSystemBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalSystem"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public LocalSystem(IWebHostEnvironment? hostingEnvironment = null)
        {
            ContentRootPath = hostingEnvironment?.ContentRootPath ?? "./";
        }

        /// <summary>
        /// Name of the file system
        /// </summary>
        public override string Name { get; } = "Local file system";

        /// <summary>
        /// Gets the order (lower numbers occur first).
        /// </summary>
        /// <value>The order.</value>
        public override int Order { get; } = int.MaxValue;

        /// <summary>
        /// Regex string used to determine if the file system can handle the path
        /// </summary>
        protected override string HandleRegexString { get; } = "^mithril://";

        /// <summary>
        /// Gets the content root path.
        /// </summary>
        /// <value>The content root path.</value>
        private string ContentRootPath { get; }

        /// <summary>
        /// Gets the directory representation for the directory
        /// </summary>
        /// <param name="path">Path to the directory</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns>The directory object</returns>
        public override IDirectory Directory(string? path, Credentials? credentials = null)
        {
            path = AbsolutePath(path);
            if (string.IsNullOrEmpty(path))
                return null!;
            return new FileCurator.DirectoryInfo(path, credentials);
        }

        /// <summary>
        /// Gets the class representation for the file
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns>The file object</returns>
        public override IFile File(string? path, Credentials? credentials = null)
        {
            path = AbsolutePath(path);
            if (string.IsNullOrEmpty(path))
                return null!;
            return new FileCurator.FileInfo(path, credentials);
        }

        /// <summary>
        /// Gets the absolute path of the variable passed in
        /// </summary>
        /// <param name="path">Path to convert to absolute</param>
        /// <returns>The absolute path of the path passed in</returns>
        protected override string AbsolutePath(string? path)
        {
            return path?.Replace("mithril://", ContentRootPath + "/wwwroot/", StringComparison.OrdinalIgnoreCase).Replace("..", "") ?? "";
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="managed">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected override void Dispose(bool managed)
        {
        }
    }
}